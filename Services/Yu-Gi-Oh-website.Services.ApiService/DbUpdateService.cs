using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;
using Yu_Gi_Oh_website.Services.ApiService.Models;
using Yu_Gi_Oh_website.Web.Data;

namespace Yu_Gi_Oh_website.Services.ApiService
{
    public class DbUpdateService : IDbUpdateService
    {
        private readonly ApplicationDbContext context;     
        private readonly HttpClient httpClient;

        public DbUpdateService(ApplicationDbContext context, HttpClient httpClient)
        {
            this.context = context;            
            this.httpClient = httpClient;
        }
        public async Task AddAllCardsToDbAsync(string imageFolder)
        {
            if (context.Cards.Any())
            {
                return;
            }
            await UpdateRacesAsync();
            await UpdateTypesAsync();
         
           
            await UpdateDbAsync(imageFolder, ApiConstantValues.allCardsString);

        }

        public async Task AddIndividualCardToDbAsync(string imageFolder, string cardName)
        {
            if (string.IsNullOrEmpty(cardName))
            {
                throw new ArgumentException("Card name cannot be empty");
            }

            if (context.Cards.Any(x => x.Name == cardName))
            {
                return;
            }


            await UpdateDbAsync(imageFolder, $"https://db.ygoprodeck.com/api/v7/cardinfo.php?name={cardName}&misc=yes");

        }

        private async Task UpdateDbAsync(string imageFolder, string apiString)
        {



            RootObject? json = await GetJsonResponse(apiString);
            var raceObjects = new List<Race>();
            var typeObjects = new List<CardType>();
            var attributeObjects = new List<CardAttribute>();




            if (json?.Data?.Count() > 1)
            {
                raceObjects = await context.Races.ToListAsync();
                typeObjects = await context.Types.ToListAsync();
                attributeObjects = await context.CardAttributes.ToListAsync();
            }
            List<Card> cards = new List<Card>();
            List<CardImage> cardImages = new List<CardImage>();
            //  File.WriteAllText("text.json", JsonConvert.SerializeObject(json));
            foreach (var cardJson in json?.Data!)
            {
                var card = new Card()
                {
                    Name = cardJson.Name,
                    Atk = cardJson.Atk,
                    Def = cardJson.Def,
                    Description = cardJson.Description,
                    CardType = await SetType(cardJson, typeObjects),
                    Race = await SetRace(cardJson, raceObjects),
                    CardAttribute = await SetAttribute(cardJson, attributeObjects),
                    Scale = cardJson.Scale,
                    LinkValue = cardJson.LinkValue,
                    Level = cardJson.Level,
                    HasEffect = cardJson.Misc[0].HasEffect,


                };

                if (card.Race.Name.StartsWith("Ignore ") || card.CardType.Name.StartsWith("Ignore "))
                {
                    string log = $"{card.Name} : Type {card.CardType.Name.Replace("Ignore ", "")}, Race: {card.Race.Name.Replace("Ignore ", "")}\n";
                    File.AppendAllText("log.txt", log);
                    continue;
                }

                var count = 1;

                foreach (var link in cardJson.ImageUrls.Select(x => x.ImageUrl))
                {
                    string path = $"{imageFolder}/{RemoveSpecialCharacters(cardJson.Name)}{count++}.jpg";
                    //  Console.WriteLine(path);

                    await DownloadImageAsync(httpClient, link!, path);

                    var cardImage = new CardImage()
                    {
                        Card = card,
                        ImageUrl = path.Replace("wwwroot", ""),
                    };

                    cardImages.Add(cardImage);
                }

                cards.Add(card);

            }
            await context.AddRangeAsync(cards);
            await context.SaveChangesAsync();
            await context.AddRangeAsync(cardImages);
            await context.SaveChangesAsync();
        }


        private async Task DownloadImageAsync(HttpClient client, string url, string path)
        {
            if (File.Exists(path))
            {
                return;
            }
            var byteArray = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(path, byteArray);

        }

        private string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_]+", "_", RegexOptions.Compiled);
        }

        private async Task UpdateRacesAsync()
        {
            var racesObjects = new HashSet<Race>();

            foreach (var race in ApiConstantValues.races)
            {
                racesObjects.Add(new Race() { Name = race });

            }

            var existingRaceNames = await context.Races.Select(x => x.Name).ToListAsync();

            racesObjects = racesObjects.Where(race => !existingRaceNames.Contains(race.Name)).ToHashSet();

            await context.AddRangeAsync(racesObjects);
            await context.SaveChangesAsync();

        }

        private async Task UpdateTypesAsync()
        {
            var typeObjects = new HashSet<CardType>();

            foreach (var type in ApiConstantValues.types)
            {
                typeObjects.Add(new CardType() { Name = type });

            }

            var existingTypeNames = await context.Types.Select(x => x.Name).ToListAsync();

            typeObjects = typeObjects.Where(type => !existingTypeNames.Contains(type.Name)).ToHashSet();

            await context.AddRangeAsync(typeObjects);
            await context.SaveChangesAsync();

        }

        private async Task<RootObject?> GetJsonResponse(string apiString)
        {

            var result = await httpClient.GetAsync(apiString);
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RootObject>(responseBody);
        }

        private async Task<Race> SetRace(CardDTO json, ICollection<Race> raceObjects)
        {
            Race? race;
            if (raceObjects.Any())
            {
                race = raceObjects.FirstOrDefault(x => x.Name == json.Race)!;

                if (race is null)
                {
                    race = new Race() { Name = $"Ignore {json.Race}" };
                }

                return race;
            }

            race = await context.Races.FirstOrDefaultAsync(x => x.Name == json.Race);

            if (race is null)
            {
                throw new ArgumentException("Race is invalid value");
            }

            return race;
        }

        private async Task<CardType> SetType(CardDTO json, ICollection<CardType> typeObjects)
        {
            CardType? type;
            if (typeObjects.Any())
            {
                type = typeObjects.FirstOrDefault(x => x.Name == json.Type)!;

                if (type is null)
                {
                    type = new CardType() { Name = $"Ignore {json.Type}" };
                }

                return type;
            }

            type = await context.Types.FirstOrDefaultAsync(x => x.Name == json.Type);

            if (type is null)
            {
                throw new ArgumentException("Type is invalid value");
            }

            return type;

        }
        private async Task<CardAttribute?> SetAttribute(CardDTO json, ICollection<CardAttribute> attributeObjects)
        {
            if (json.Attribute is null)
            {
                return null;
            }

            CardAttribute? attribute;

            if (attributeObjects.Any())
            {
                attribute = attributeObjects.FirstOrDefault(o => o.Name == json.Attribute);

                if (attribute is null && !attributeObjects.Select(x => x.Name).Contains(json.Attribute))
                {
                    attribute = new CardAttribute() { Name = json.Name };
                    attributeObjects.Add(attribute);

                }

                return attribute;


            }

            attribute = await context.CardAttributes.FirstOrDefaultAsync(x => x.Name == json.Attribute);

            if (attribute is null)
            {

                attribute = new CardAttribute() { Name = json.Attribute };
                attributeObjects.Add(attribute);

            }

            return attribute;




        }
    }
}
