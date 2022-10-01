using AspNetCoreTemplate.Data.Seeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Common;
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
            var subCattegoryList = new List<SubCattegory>()
            {
                new SubCattegory { Name = "Yu-Gi-Oh! Duel Monsters", Cattegory = animeCattegory,Description="Discussions about the Yu-Gi-Oh! Duel Monsters Anime."},
                new SubCattegory { Name = "Yu-Gi-Oh! GX", Cattegory = animeCattegory, Description = "Discussions about the Yu-Gi-Oh! GX Anime." },
                new SubCattegory { Name = "Yu-Gi-Oh! 5DS" , Cattegory = animeCattegory , Description = "Discussions about the Yu-Gi-Oh! 5DS Anime." },
                new SubCattegory { Name = "Yu-Gi-Oh! Zexal" , Cattegory = animeCattegory , Description = "Discussions about the Yu-Gi-Oh! Zexal Anime." },
                new SubCattegory { Name = "Yu-Gi-Oh! Arc-V" , Cattegory = animeCattegory , Description = "Discussions about the Yu-Gi-Oh! Arc-V Anime." },
                new SubCattegory { Name = "Yu-Gi-Oh! VRAINS" , Cattegory = animeCattegory , Description = "Discussions about the Yu-Gi-Oh! VRAINS Anime." },
                new SubCattegory { Name = "Yu-Gi-Oh! Sevens" , Cattegory = animeCattegory , Description = "Discussions about the Yu-Gi-Oh! Sevens Anime." },
                new SubCattegory { Name = "Yu-Gi-Oh! Go Rush!!" , Cattegory = animeCattegory , Description = "Discussions about the Yu-Gi-Oh! Go Rush!! Anime." },

                new SubCattegory { Name = "General Discussions",Cattegory=masterDuelCattegory, Description = "Discussions about Yu-Gi-Oh! Master Duel - gameplay, announcements, features and bugs." },
                new SubCattegory { Name = "Meta Discussions" , Cattegory = masterDuelCattegory , Description = "Discussions about the current Meta in Master Duel." },
                new SubCattegory { Name = "Deck Discussions", Cattegory = masterDuelCattegory, Description = "Discussions about Decks - Share Decks, Deck Strategies and Ideas about DeckBuilding." },
                new SubCattegory { Name = "Events", Cattegory = masterDuelCattegory , Description = "Discussion about current and upcoming Master Duel in-game Events." },
                new SubCattegory { Name = "Tournaments", Cattegory = masterDuelCattegory ,Description = "Promote or organize Master Duel Tournaments." },

                new SubCattegory { Name = "General Discussions", Cattegory = duelLinksCattegory, Description = "Discussions about Yu-Gi-Oh! Duel Links - gameplay, announcements, features and bugs." },
                new SubCattegory { Name = "Meta Discussions", Cattegory = duelLinksCattegory ,Description = "Discussions about the current Meta in Duel Links." },
                new SubCattegory { Name = "Deck Discussions", Cattegory = duelLinksCattegory ,Description = "Discussions about Decks - Share Decks, Deck Strategies and Ideas about DeckBuilding." },
                new SubCattegory { Name = "Events", Cattegory = duelLinksCattegory , Description = "Discussion about current and upcoming Duel Links in-game Events." },
                new SubCattegory { Name = "Tournaments", Cattegory = duelLinksCattegory , Description = "Promote or organize Duel Links Tournaments." },

                new SubCattegory { Name = "General Discussions", Cattegory = tcgCattegory , Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Meta Discussions", Cattegory = tcgCattegory , Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Deck Discussions", Cattegory = tcgCattegory , Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Card Trading", Cattegory = tcgCattegory , Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Live Events", Cattegory = tcgCattegory, Description = "abcdadawdadsadwad" },

                new SubCattegory { Name = "Other Anime", Cattegory = miscellaneousCattegory, Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Other Games", Cattegory = miscellaneousCattegory, Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Movies / TV Shows", Cattegory = miscellaneousCattegory, Description = "abcdadawdadsadwad" },
                new SubCattegory { Name = "Spam", Cattegory = miscellaneousCattegory, Description = "abcdadawdadsadwad" }
            };

            foreach (var item in subCattegoryList)
            {
                item.Slug = $"{item.Cattegory.Name.ToUrlSlug()}-{item.Name.ToUrlSlug()}";
            }
            await dbContext.SubCattegories.AddRangeAsync(subCattegoryList);
        }
    }
}
