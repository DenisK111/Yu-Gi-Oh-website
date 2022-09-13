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
using Yu_Gi_Oh_website.Models.Enums;
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
            await UpdateTypesAsync();
            await UpdateExactTypesAsync();
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

            RootObject? json = await GetJsonResponseAsync(apiString);
            var typeObjects = new List<CardType>();
            var exactCardTypeObjects = new List<ExactCardType>();
            var attributeObjects = new List<CardAttribute>();


            if (json is null || json.Data is null || !json.Data!.Any())
            {
                return;
                
            }
            
            typeObjects = await context.Types.ToListAsync();
            exactCardTypeObjects = await context.ExactCardTypes.ToListAsync();
            attributeObjects = await context.CardAttributes.ToListAsync();

            
            List<Card> cards = new List<Card>();
            List<CardImage> cardImages = new List<CardImage>();
            //  File.WriteAllText("text.json", JsonConvert.SerializeObject(json));
            foreach (var cardJson in json?.Data!)
            {
                var card = new Card()
                {
                    Name = cardJson.Name,
                    Atk = cardJson.Misc[0].QuestionAtk ? -1 : cardJson.Atk,
                    Def = cardJson.Misc[0].QuestionDef ? -1 : cardJson.Def,
                    Description = cardJson.Description,
                    CardType = SetCardType(cardJson),
                    Type = await SetTypeAsync(cardJson, typeObjects),
                    ExactCardType = await SetExactCardTypeAsync(cardJson, exactCardTypeObjects),
                    CardAttribute = await SetAttributeAsync(cardJson, attributeObjects),
                    Scale = cardJson.Scale,
                    LinkValue = cardJson.LinkValue,
                    Level = cardJson.Level,



                };

                if (card.Type.Name.StartsWith("Ignore ") || card.ExactCardType.Name.StartsWith("Ignore "))
                {
                    string log = $"{card.Name} : Type {card.ExactCardType.Name.Replace("Ignore ", "")}, Race: {card.Type.Name.Replace("Ignore ", "")}\n";
                    File.AppendAllText("log.txt", log);
                    continue;
                }

                var count = 1;

                foreach (var link in cardJson.ImageUrls.Select(x => x.ImageUrl))
                {
                    string path = $"{imageFolder}/{RemoveSpecialCharacters(cardJson.Name)}{count++}.jpg";
                    //  Console.WriteLine(path);

                    //await DownloadImageAsync(httpClient, link!, path);

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

        private async Task<ExactCardType> SetExactCardTypeAsync(CardApiDTO cardJson, List<ExactCardType> exactCardTypeObjects)
        {
            ExactCardType? type;
            if (exactCardTypeObjects.Any())
            {
                type = exactCardTypeObjects.FirstOrDefault(x => x.Name == cardJson.Type)!;

                if (type is null)
                {
                    type = new ExactCardType() { Name = $"Ignore {cardJson.Type}" };
                }

                return type;
            }

            type = await context.ExactCardTypes.FirstOrDefaultAsync(x => x.Name == cardJson.Type);

            if (type is null)
            {
                throw new ArgumentException("Type is invalid value");
            }

            return type;
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

        private async Task UpdateTypesAsync()
        {
            var typeObjects = new HashSet<CardType>();

            foreach (var race in ApiConstantValues.types)
            {
                typeObjects.Add(new CardType() { Name = race });

            }

            var existingTypeNames = await context.Types.Select(x => x.Name).ToListAsync();

            typeObjects = typeObjects.Where(type => !existingTypeNames.Contains(type.Name)).ToHashSet();

            await context.AddRangeAsync(typeObjects);
            await context.SaveChangesAsync();

        }

        private async Task UpdateExactTypesAsync()
        {
            var typeObjects = new HashSet<ExactCardType>();

            foreach (var type in ApiConstantValues.cardTypeMapping.SelectMany(x => x.Value))
            {
                typeObjects.Add(new ExactCardType() { Name = type });

            }

            var existingTypeNames = await context.ExactCardTypes.Select(x => x.Name).ToListAsync();

            typeObjects = typeObjects.Where(type => !existingTypeNames.Contains(type.Name)).ToHashSet();

            await context.AddRangeAsync(typeObjects);
            await context.SaveChangesAsync();

        }


        private async Task<RootObject?> GetJsonResponseAsync(string apiString)
        {

            var result = await httpClient.GetAsync(apiString);
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RootObject>(responseBody);
        }


        private async Task<CardType> SetTypeAsync(CardApiDTO json, ICollection<CardType> typeObjects)
        {
            this.CheckForSpellTrapTypes(json);
            CardType? type;
            if (typeObjects.Any())
            {
                type = typeObjects.FirstOrDefault(x => x.Name == json.Race)!;

                if (type is null)
                {
                    type = new CardType() { Name = $"Ignore {json.Race}" };
                }

                return type;
            }

            type = await context.Types.FirstOrDefaultAsync(x => x.Name == json.Race);

            if (type is null)
            {
                throw new ArgumentException("Race is invalid value");
            }

            return type;
        }

        private void CheckForSpellTrapTypes(CardApiDTO json)
        {
            if (json.Type == "Spell Card")
            {
                json.Race = $"{json.Race} Spell";
            }

            else if (json.Type == "Trap Card")
            {
                json.Race = $"{json.Race} Trap";
            }


        }

        private CardTypeEnum SetCardType(CardApiDTO json)
        {
            string type = json.Type;
            var typeValue = ApiConstantValues.cardTypeMapping.FirstOrDefault(t => t.Value.Contains(type)).Key;
            return typeValue;

        }
        private async Task<CardAttribute?> SetAttributeAsync(CardApiDTO json, ICollection<CardAttribute> attributeObjects)
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
                    attribute = new CardAttribute() { Name = json.Attribute };
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
