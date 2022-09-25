using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Http;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;
using Yu_Gi_Oh_website.Services.ApiService;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Implementations;
using Yu_Gi_Oh_website.Web.AutoMapper;
using Yu_Gi_Oh_website.Web.Data;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Implementations;

namespace Yu_Gi_Oh_website.Web
{
    public static class DIContainer
    {
        // Add services to the container.
        public static WebApplication ConfigureAndBuild(this WebApplicationBuilder builder)
        {
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

            builder.Services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });
            builder.Services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = builder.Configuration.GetConnectionString(
                    "DefaultConnection");
                options.SchemaName = "dbo";
                options.TableName = "TestCache";
            });

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

            })
                .AddSessionStateTempDataProvider();

            builder.Services.AddAutoMapper(typeof(CardProfile));

            builder.Services.AddTransient<IDbUpdateService,DbUpdateService>();
            builder.Services.AddTransient<ICardCollectionService, CardCollectionService>();
            builder.Services.AddTransient<HttpClient>();
            builder.Services.AddScoped<IFilterService, FilterService>();
            builder.Services.AddScoped<ISortingService, SortingService>();
            builder.Services.AddScoped<IHomePageService, HomePageService>();



            return builder.Build();
        }
    }
}
