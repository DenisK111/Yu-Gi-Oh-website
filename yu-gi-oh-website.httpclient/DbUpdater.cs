using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yu_gi_oh_website.httpclient
{
    public class DbUpdater
    {
        public async Task DbUpdate(DateTime inputStartDate, DateTime? inputEndDate = null)
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

           var json = JsonConvert.DeserializeObject<Root>(responseBody);

            File.WriteAllText("text.json", JsonConvert.SerializeObject(json));

            using (httpClient)
            {
                foreach (var linkArray in json!.Data.Select(x => new { Images = x.CardImages, Name = x.Name }))
                {
                    var count = 1;
                    var name = linkArray.Name;
                    var listOfIMages = linkArray.Images.Select(x => x.ImageUrl);

                    foreach (var link in listOfIMages)
                    {
                        string path = $"../../../Images/{name}{count++}.jpg";

                        await DownloadImage(httpClient, link, path);
                    }
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
