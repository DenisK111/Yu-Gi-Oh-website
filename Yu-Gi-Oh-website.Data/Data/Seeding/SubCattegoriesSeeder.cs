using AspNetCoreTemplate.Data.Seeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Web.Data;

namespace Yu_Gi_Oh_website.Data.Data.Seeding
{
    public class SubCattegoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.SubCattegories.Any())
            {
                return;
            }

            var cattegories = dbContext.Cattegories.ToList();
            var animeCattegory = dbContext.Cattegories.First(x => x.Name == CattegoryNames.Anime);
            var masterDuelCattegory = dbContext.Cattegories.First(x => x.Name == CattegoryNames.MasterDuel);
            var duelLinksCattegory = dbContext.Cattegories.First(x => x.Name == CattegoryNames.DuelLinks);
            var tcgCattegory = dbContext.Cattegories.First(x => x.Name == CattegoryNames.TCG);
            var miscellaneousCattegory = dbContext.Cattegories.First(x => x.Name == CattegoryNames.Miscellaneous);

            await dbContext.SubCattegories.AddRangeAsync(
                new SubCattegory { Name = "Yu-Gi-Oh! Duel Monsters", Cattegory = animeCattegory,Description="abcdadawdadsadwad" },
                new SubCattegory { Name = "Yu-Gi-Oh! GX", Cattegory = animeCattegory, Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Yu-Gi-Oh! 5DS" , Cattegory = animeCattegory , Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Yu-Gi-Oh! Zexal" , Cattegory = animeCattegory , Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Yu-Gi-Oh! Arc-V" , Cattegory = animeCattegory , Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Yu-Gi-Oh! VRAINS" , Cattegory = animeCattegory , Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Yu-Gi-Oh! Go Rush!!" , Cattegory = animeCattegory , Description = "abcdadawdadsadwad" },
                
                new SubCattegory { Name = "General Discussions",Cattegory=masterDuelCattegory, Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Meta Discussions" , Cattegory = masterDuelCattegory , Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Deck Discussions", Cattegory = masterDuelCattegory, Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Tournaments", Cattegory = masterDuelCattegory ,Description = "abcdadawdadsadwad" },

                new SubCattegory { Name = "General Discussions", Cattegory = duelLinksCattegory, Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Meta Discussions", Cattegory = duelLinksCattegory ,Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Deck Discussions", Cattegory = duelLinksCattegory ,Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Events", Cattegory = duelLinksCattegory , Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Tournaments", Cattegory = duelLinksCattegory , Description = "abcdadawdadsadwad" },

                new SubCattegory { Name = "General Discussions", Cattegory = tcgCattegory , Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Meta Discussions", Cattegory = tcgCattegory , Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Deck Discussions", Cattegory = tcgCattegory , Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Card Trading", Cattegory = tcgCattegory , Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Live Events", Cattegory = tcgCattegory, Description = "abcdadawdadsadwad" },

                new SubCattegory { Name = "Other Anime", Cattegory = miscellaneousCattegory, Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Other Games", Cattegory = miscellaneousCattegory, Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Movies / TV Shows", Cattegory = miscellaneousCattegory, Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Spam", Cattegory = miscellaneousCattegory, Description = "abcdadawdadsadwad" }

                );
        }
    }
}
