using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Models.Forum.Models;

namespace Yu_Gi_Oh_website.Services.Forum.Contracts
{
	public interface IEntityByIdService
	{
		Task<ApplicationUser?> GetAuthorByUserName(string userName);
		Task<ForumThread?> GetThreadById(int threadId);
	}
}