using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Models;
using Yu_Gi_Oh_website.Web.Data;

namespace Yu_Gi_Oh_website.Services.Forum.Implementations
{
	public class CattegoryService : ICattegoryService
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public CattegoryService(ApplicationDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<IEnumerable<CattegoryIdNameDto>> GetCattegoryIdNameAsync()
		{
			var result = mapper.ProjectTo<CattegoryIdNameDto>(context.Cattegories);

            return await result.ToListAsync();
		}

        public async Task<ICollection<CattegoryDto>> GetallCattegoriesAsync()
        {
            var cattegories = context.Cattegories.AsQueryable();
            var dict = await mapper.ProjectTo<CattegoryDto>(cattegories).ToListAsync();
            return dict;
        }
    }
}
