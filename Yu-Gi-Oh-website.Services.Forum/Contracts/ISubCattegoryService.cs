using Yu_Gi_Oh_website.Services.Forum.Models;

namespace Yu_Gi_Oh_website.Services.Forum.Contracts
{
    public interface ISubCattegoryService
    {
        Task<FullSubCattegoryDto> GetByIdAsync(int Id);
    }
}
