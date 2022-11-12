using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Yu_Gi_Oh_website.Data.Repositories.Contracts;
using Yu_Gi_Oh_website.Models.Configurations;
using Yu_Gi_Oh_website.Models.Forum.Models;

namespace Yu_Gi_Oh_website.Data.Repositories.Implementations
{
	public class MongoVisitorCountRepository : IVisitorCountRepository
	{
        private readonly IMongoCollection<VisitorCount> visitorCountCollection;       

        public MongoVisitorCountRepository(IOptionsMonitor<MongoDbSettings> mongoSettings)
        {
            var settings = MongoClientSettings.FromConnectionString(mongoSettings.CurrentValue.ConnectionString);
            string databaseName = mongoSettings.CurrentValue.DatabaseName;
            string collectionName = mongoSettings.CurrentValue.CollectionVisitorCount;
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(settings);

            var database = client.GetDatabase(databaseName);
            this.visitorCountCollection = database.GetCollection<VisitorCount>(collectionName);
            
        }


        public async Task AddOrUpdateAsync(string path, string ipAddress, int threadId)
		{
            var document = await (await visitorCountCollection.FindAsync(x => x.Path == path)).FirstOrDefaultAsync();
            if (document == default(VisitorCount))
            {
                await visitorCountCollection.InsertOneAsync(new VisitorCount()
                {
                    IpAddresses = new List<string>() { ipAddress },
                    Path = path,
                    ThreadId = threadId,                    
                });
                return;
            }

            if (document.IpAddresses.Contains(ipAddress))
            {
                return;
            }
            document.IpAddresses.Add(ipAddress);
            var update = Builders<VisitorCount>.Update
                        .Set(p => p.IpAddresses, document.IpAddresses);


            await visitorCountCollection.UpdateOneAsync(x => x == document, update);
		}

		public async Task<int> GetTotalCountByPathAsync(int threadID)
		{
            var totalCount = (await (await visitorCountCollection.FindAsync(x => x.ThreadId == threadID))
                .FirstOrDefaultAsync())
                ?.IpAddresses.Count() ?? 0;
            return totalCount;
        }

        public async Task<Dictionary<int, int>> GetTotalCountByThreadIdsAsync(IEnumerable<int> threadIds)
        {
            var allDocuments = await (await visitorCountCollection.FindAsync(x => threadIds.Contains(x.ThreadId))).ToListAsync();

            if (!allDocuments.Any())
            {
                return new Dictionary<int, int>();
            }

            return allDocuments.Select(x => new 
            { 
            Key = x.ThreadId,
            Value = x.IpAddresses.Count()
            
            })
                .ToDictionary(x=>x.Key,y=>y.Value);
        }
    }
}
