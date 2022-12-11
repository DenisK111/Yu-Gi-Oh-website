using Yu_Gi_Oh_website.Services.Forum.Models;

namespace Yu_Gi_Oh_website.Services.Forum.Contracts
{
	public interface ICattegoryService
	{
		Task<IEnumerable<CattegoryIdNameDto>> GetCattegoryIdNameAsync();
        public Task<ICollection<CattegoryDto>> GetallCattegoriesAsync();


    }
}
