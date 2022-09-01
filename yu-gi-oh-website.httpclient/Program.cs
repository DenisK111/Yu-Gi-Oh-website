using Newtonsoft.Json;
using System.Globalization;

namespace yu_gi_oh_website.httpclient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var startDate = DateTime.ParseExact("01/01/2000", "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(DateTime.UtcNow.Date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            await new DbUpdater().DbUpdate(startDate,endDate);

        }

       
    }
}