using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Yu_Gi_Oh_website.Services;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Web.Data;

namespace Yu_Gi_Oh_website.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.ConfigureAndBuild();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                // TODO look into DI Error with Db
             new ApplicationDbContext().Database.Migrate();
                
                
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}