using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Data.Repositories.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Contracts;

namespace Yu_Gi_Oh_website.Services.Forum.Implementations
{
	public class VisitorCountService : IVisitorCountService
	{
		private readonly IVisitorCountRepository visitorCountRepository;

		public VisitorCountService(IVisitorCountRepository visitorCountRepository)
		{
			this.visitorCountRepository = visitorCountRepository;
		}
		public async Task AddOrUpdateAsync(string path, string ipAddress,int threadId)
		{
			await visitorCountRepository.AddOrUpdateAsync(path, ipAddress,threadId);
		}		

		public async Task<Dictionary<int, int>> GetTotalCountByThreadIdsAsync(IEnumerable<int> threadIds)
		{
            return await visitorCountRepository.GetTotalCountByThreadIdsAsync(threadIds);
        }
	}
}
