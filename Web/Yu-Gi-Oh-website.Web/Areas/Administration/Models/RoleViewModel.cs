using System.ComponentModel.DataAnnotations;

namespace Yu_Gi_Oh_website.Web.Areas.Administration.Models
{
	public class RoleViewModel
	{
		[Required]
		public string UserId { get; set; } = null!;
		[Required]
		public string Role { get; set; } = null!;
	}
}
