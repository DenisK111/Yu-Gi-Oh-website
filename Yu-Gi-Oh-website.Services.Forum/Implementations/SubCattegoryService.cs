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
    public class SubCattegoryService : ISubCattegoryService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SubCattegoryService(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<FullSubCattegoryDto> GetByIdAsync(int id)
        {
            var subCattegory = await context.SubCattegories
                .Include(x => x.Threads)
                .ThenInclude(x=>x.Author)
                .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == id);
               
            var result = mapper.Map<FullSubCattegoryDto>(subCattegory);
            return result;

        }
    }
}
