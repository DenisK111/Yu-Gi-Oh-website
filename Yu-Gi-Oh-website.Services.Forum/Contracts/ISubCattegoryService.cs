using Yu_Gi_Oh_website.Services.Forum.Models;

namespace Yu_Gi_Oh_website.Services.Forum.Contracts
{
    public interface ISubCattegoryService
    {
        Task<FullSubCattegoryDto> GetByIdAsync(int Id,int Page,int itemsToTake);

        Task<IEnumerable<SubCattegoryInfoDto>> GetSubCattegoryAdminDetailsAsync();

        Task<bool> AddSubCattegoryAsync(string subCattegoryName,string description, int cattegoryId);        

        Task<bool> EditSubCattegoryAsync(int id, string name, string description,int cattegoryId,bool isDeleted);
                     

        
    }
}
