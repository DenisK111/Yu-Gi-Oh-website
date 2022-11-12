using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Models;
using Yu_Gi_Oh_website.Web.Data;

namespace Yu_Gi_Oh_website.Services.Forum.Implementations
{
    public class SubCattegoryService : ISubCattegoryService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IVisitorCountService visitorCountService;

        public SubCattegoryService(ApplicationDbContext context, IMapper mapper,IVisitorCountService visitorCountService)
        {
            this.context = context;
            this.mapper = mapper;
            this.visitorCountService = visitorCountService;
        }
        public async Task<FullSubCattegoryDto> GetByIdAsync(int id, int currentPage,int itemsToTake)
        {
            var result = await mapper
                .ProjectTo<FullSubCattegoryDto>(context.SubCattegories.Where(x => x.Id == id))
                .Select( x=>new FullSubCattegoryDto()
                {
                    Id = x.Id,
                    Cattegory=x.Cattegory,
                    Description=x.Description,
                    ImageUrl=x.ImageUrl,
                    Name=x.Name,
                    Slug=x.Slug,
                    Threads= x.Threads.OrderByDescending(y=>y.ModifiedOn).Skip(itemsToTake * (currentPage - 1)).Take(itemsToTake).ToList(),
                    TotalCount = x.Threads.Count(),
                })
                .FirstOrDefaultAsync();

            var threadIdViewsDictionary = await visitorCountService
                .GetTotalCountByThreadIdsAsync(result!.Threads.Select(x => x.Id));
            foreach (var (key,value) in threadIdViewsDictionary)
            {
                result.Threads.First(x => x.Id == key).Views = value;                
            }
            return result;
        }
    }
}
