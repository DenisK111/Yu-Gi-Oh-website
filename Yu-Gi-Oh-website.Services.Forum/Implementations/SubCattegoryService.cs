using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Yu_Gi_Oh_website.Common;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Services.Contracts;
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
        private readonly ISoftDeleteService<SubCattegory> softDeleteService;
        private readonly ICattegoryService cattegoryService;

        public SubCattegoryService(ApplicationDbContext context, IMapper mapper, IVisitorCountService visitorCountService, ICattegoryService cattegoryService, ISoftDeleteService<SubCattegory> softDeleteService)
        {
            this.context = context;
            this.mapper = mapper;
            this.visitorCountService = visitorCountService;
            this.cattegoryService = cattegoryService;
            this.softDeleteService = softDeleteService;
        }

        public async Task<bool> AddSubCattegoryAsync(string subCattegoryName, string description, int cattegoryId)
        {
            var cattegory = await context.Cattegories.FindAsync(cattegoryId);
            if (cattegory is null
                   || context.SubCattegories.Any(x => x.Name == subCattegoryName && x.CattegoryId == cattegoryId))
            {
                return false;
            }           

            await context.AddAsync(new SubCattegory()
            {
                CattegoryId = cattegoryId,
                Description = description,
                Name = subCattegoryName,
                Slug= $"{cattegory.Name.ToUrlSlug()}-{subCattegoryName.ToUrlSlug()}"
            });

            await context.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> EditSubCattegoryAsync(int id, string name, string description,int cattegoryId,bool isDeleted)
        {
            var subCattegory = await context.SubCattegories.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == id);

            if (subCattegory is null) return false;

            subCattegory.Name = name;
            subCattegory.Description = description;
            subCattegory.CattegoryId = cattegoryId;

            if (subCattegory.IsDeleted != isDeleted)
            {
                if (isDeleted)
                {
                    softDeleteService.SoftDelete(subCattegory);

                }
                else softDeleteService.Undelete(subCattegory);

            }

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<FullSubCattegoryDto> GetByIdAsync(int id, int Page, int itemsToTake)
        {
            var result = await mapper
                .ProjectTo<FullSubCattegoryDto>(context.SubCattegories.Where(x => x.Id == id))
                .Select(x => new FullSubCattegoryDto()
                {
                    Id = x.Id,
                    Cattegory = x.Cattegory,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Name = x.Name,
                    Slug = x.Slug,
                    Threads = x.Threads.OrderByDescending(y => y.ModifiedOn).Skip(itemsToTake * (Page - 1)).Take(itemsToTake).ToList(),
                    TotalCount = x.Threads.Count(),
                })
                .FirstOrDefaultAsync();

            var threadIdViewsDictionary = await visitorCountService
                .GetTotalCountByThreadIdsAsync(result!.Threads.Select(x => x.Id));
            foreach (var (key, value) in threadIdViewsDictionary)
            {
                result.Threads.First(x => x.Id == key).Views = value;
            }
            return result;
        }

        public async Task<IEnumerable<SubCattegoryInfoDto>> GetSubCattegoryAdminDetailsAsync()
        {
            var result = await mapper.ProjectTo<SubCattegoryInfoDto>(context.SubCattegories.Include(x=>x.Cattegory).IgnoreQueryFilters()).ToListAsync();
            return result;                                
        }   
                
    }
}
