using Yu_Gi_Oh_website.Web.Data;

namespace AspNetCoreTemplate.Data.Seeding
{
    public interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
    }
}
