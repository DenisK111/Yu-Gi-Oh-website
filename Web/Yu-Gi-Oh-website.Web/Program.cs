using AspNetCoreTemplate.Data.Seeding;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yu_Gi_Oh_website.Common.Settings;
using Yu_Gi_Oh_website.Models.Configurations;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Services.ApiService;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Implementations;
using Yu_Gi_Oh_website.Services.Implementations;
using Yu_Gi_Oh_website.Web.AutoMapper;
using Yu_Gi_Oh_website.Web.Data;
using Yu_Gi_Oh_website.Web.Middleware;
using Yu_Gi_Oh_website.Web.Extentension;
using NToastNotify;
using CloudinaryDotNet;
using Cloudinary = CloudinaryDotNet.Cloudinary;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Text;

namespace Yu_Gi_Oh_website.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.AddSerilog(logger);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {

                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection(nameof(MongoDbSettings)));          

            /// ADD CLOUDINARY
            
            Account account = new Account(
                        builder.Configuration["Cloudinary:ApiName"],
                        builder.Configuration["Cloudinary:ApiKey"],
                        builder.Configuration["Cloudinary:ApiSecret"]);

            var cloudinary = new Cloudinary(account);

            builder.Services.AddSingleton(cloudinary);

            builder.Services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });
     
            builder.Services.AddMemoryCache();           

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddResponseCompression();

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

            }).AddNToastNotifyToastr(new NToastNotify.ToastrOptions()
            {
                ProgressBar = true,
                PositionClass = ToastPositions.TopCenter,
                TimeOut = 3000,
               
            })
                .AddSessionStateTempDataProvider();

            builder.Services.AddAntiforgery(options =>
            {
                options.HeaderName = AntiforgerySettings.HeaderName;
            });

            builder.Services.AddAutoMapper(typeof(CardProfile));            

            builder.Services.AddSingleton<HttpClient>();
            builder.Services.RegisterServices();
            builder.Services.RegisterRepositories();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();

                using (var serviceScope = app.Services.CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetRequiredService<Data.ApplicationDbContext>();
                    dbContext.Database.Migrate();
                    await new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider);
                    await new GetApiDataAndUpdateDbService(dbContext, new HttpClient()).AddAllCardsToDbAsync(ApiConstantValues.imagePath);
                }
                            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseResponseCompression();
               

            }
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseNToastNotify();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                   ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseNToastNotify();
            //app.UseMiddleware<AntiXssMiddleware>();
            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();           
            app.UseCookiePolicy();

            app.Run();
        }
    }
}