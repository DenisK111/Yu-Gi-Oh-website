using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Models;
using Yu_Gi_Oh_website.Web.Data;

namespace Yu_Gi_Oh_website.Services.Forum.Implementations
{
    public class HomePageService : IHomePageService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public HomePageService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<ICollection<CattegoryDto>> GetallCattegories()
        {
            var cattegories = context.Cattegories.AsQueryable();
            var dict = await mapper.ProjectTo<CattegoryDto>(cattegories).ToListAsync();
            return dict;
        }
    }
}
