using Yu_Gi_Oh_website.Data.Data.Seeding;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Web.Data;

namespace AspNetCoreTemplate.Data.Seeding
{


    internal class CattegoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cattegories.Any())
            {
                return;
            }

            await dbContext.Cattegories.AddRangeAsync(
                new Cattegory { Name = CattegoryNames.Anime,ImageUrl = ImageUrls.Anime},
                new Cattegory { Name = CattegoryNames.MasterDuel,ImageUrl=ImageUrls.MasterDuel},
                new Cattegory { Name = CattegoryNames.DuelLinks, ImageUrl = ImageUrls.DuelLinks },
                new Cattegory { Name = CattegoryNames.TCG, ImageUrl = ImageUrls.TCG },
                new Cattegory { Name = CattegoryNames.Miscellaneous,ImageUrl=ImageUrls.Miscellaneous }
                );
        }
    }
}