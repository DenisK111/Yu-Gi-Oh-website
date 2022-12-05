using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Services.Forum.Models;

namespace Yu_Gi_Oh_website.Services.Forum.Contracts
{
	public interface ICattegoryService
	{
		Task<IEnumerable<CattegoryIdNameDto>> GetCattegoryIdNameAsync();

		
	}
}
