﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu_Gi_Oh_website.Data.Repositories.Contracts
{
	public interface IVisitorCountRepository
	{
		Task AddOrUpdateAsync(string path, string ipAddress,int threadId);
		Task<Dictionary<int,int>> GetTotalCountByThreadIdsAsync(IEnumerable<int> threadIds);
	}
}
