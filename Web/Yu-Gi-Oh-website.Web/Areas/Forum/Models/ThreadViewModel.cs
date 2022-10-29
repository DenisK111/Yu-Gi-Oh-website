using Yu_Gi_Oh_website.Services.Forum.Models;
using Yu_Gi_Oh_website.Web.Models;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Models
{
	public class ThreadViewModel : ThreadDto
	{
		public PageViewModel Paging { get; set; }
	}
}
