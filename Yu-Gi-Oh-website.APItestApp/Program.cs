using Yu_Gi_Oh_website.Services.ApiService;
using Yu_Gi_Oh_website.Web.Data;

namespace yu_gi_oh_website.httpclient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {           
         await new YGOApiService(new ApplicationDbContext(),new HttpClient()).AddAllCardsToDbAsync("../../../Images");
        }
    }
}