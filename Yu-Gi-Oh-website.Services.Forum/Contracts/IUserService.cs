using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Services.Forum.Models;

namespace Yu_Gi_Oh_website.Services.Forum.Contracts
{
	public interface IUserService
	{
		Task<UserInfoDto?> GetUserWithRolesAsync(string userID);

		Task<IEnumerable<UserInfoDto>> GetAllUsersWithRolesAsync();

		Task<bool> AddUserToRoleAsync(string userID, string role);

		Task<bool> RemoveUserFromRoleAsync(string userID, string role);
	}
}
