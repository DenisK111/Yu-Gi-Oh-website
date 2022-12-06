using Yu_Gi_Oh_website.Services.Forum.Models;

namespace Yu_Gi_Oh_website.Web.Areas.Administration.Models
{
	public class UserRoleModifierViewModel
	{
		public UserInfoDto User { get; set; } = null!;

		public IEnumerable<string> AllRoles { get; set; } = null!;
	}
}
