using Newtonsoft.Json;
using System.Globalization;
using Yu_Gi_Oh_website.Web.Data;

namespace yu_gi_oh_website.httpclient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
           
           
           
            var startDate = DateTime.ParseExact("01/01/2000", "MM/dd/yyyy",CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(DateTime.UtcNow.Date.ToString("MM/dd/yyyy",CultureInfo.InvariantCulture), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            await new DbUpdater(new ApplicationDbContext()).DbUpdate(ApiCallParameters.imagePath,startDate, endDate);

        }

       
    }
}