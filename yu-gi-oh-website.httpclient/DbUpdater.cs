using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yu_gi_oh_website.httpclient.Models;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;
using Yu_Gi_Oh_website.Web.Data;

namespace yu_gi_oh_website.httpclient
{
    public class DbUpdater
    {
        private readonly ApplicationDbContext context;

        public DbUpdater(ApplicationDbContext context )
        {
            this.context = context;
        }
        public async Task DbUpdate(string imagesPath, DateTime inputStartDate, DateTime? inputEndDate = null)
        {
            
            if (!inputEndDate.HasValue)
            {
                inputEndDate = DateTime.Now;
            }
            var httpClient = new HttpClient();
            var apiParameters = new ApiCallParameters();
            var result = await httpClient.GetAsync(apiParameters.GetAllCardsString(inputStartDate, inputEndDate));
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<RootObject>(responseBody);

            //  File.WriteAllText("text.json", JsonConvert.SerializeObject(json));

            using (httpClient)
            {
                List<Card> cards = new List<Card>();
                
                foreach (var cardJson in json!.Data!)
                {
                   var card =  new Card()
                    {
                        Name=cardJson.Name,
                        Atk=cardJson.Atk,
                        Def=cardJson.Def,
                        Description=cardJson.Description,
                        CardType= context.Types.FirstOrDefault(x=>x.Name == cardJson.Type) ?? new CardType() {Name=cardJson.Type },
                        Race= context.Races.FirstOrDefault(x => x.Name == cardJson.Race) ?? new Race() { Name=cardJson.Race},
                        CardAttribute = cardJson.Attribute == null 
                        ? null
                        : (context.CardAttributes.FirstOrDefault(x => x.Name == cardJson.Attribute)
                        ?? new CardAttribute() {Name=cardJson.Attribute}),
                        Scale = cardJson.Scale,
                        LinkValue = cardJson.LinkValue,
                        Level=cardJson.Level,
                        HasEffect = cardJson.Misc[0].HasEffect,


                    };
                    await context.AddAsync(card);
                    await context.SaveChangesAsync();

                    var count = 1;
                    var name = cardJson.Name;
                    var listOfIMages = cardJson.ImageUrls.Select(x => x.ImageUrl);
                    List<CardImage> cardImages = new List<CardImage>();
                    foreach (var link in listOfIMages)
                    {
                        string path = $"{imagesPath}/{name}{count++}.jpg";

                        await DownloadImage(httpClient, link!, path);

                        var cardImage = new CardImage()
                        {
                            Card = card,
                            ImageUrl = path
                        };

                        cardImages.Add(cardImage);
                    }

                    await context.AddRangeAsync(cardImages);
                    await context.SaveChangesAsync();
                }
                    
            }
        }
        private async Task DownloadImage(HttpClient client, string url, string path)
        {
            var array = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(path, array);

        }
    }
}
