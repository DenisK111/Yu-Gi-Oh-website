using Yu_Gi_Oh_website.Services.Forum.Models;
using Yu_Gi_Oh_website.Web.Models;
using Yu_Gi_Oh_website.Web.Models.Contracts;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Models
{
	public class ThreadViewModel : ThreadDto,IPagingModel
	{
		public PageViewModel Paging { get; set; } = null!;
	}
}
