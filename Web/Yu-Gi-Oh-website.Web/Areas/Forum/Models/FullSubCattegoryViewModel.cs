using Yu_Gi_Oh_website.Services.Forum.Models;
using Yu_Gi_Oh_website.Web.Models;
using Yu_Gi_Oh_website.Web.Models.Contracts;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Models
{
    public class FullSubCattegoryViewModel : IPagingModel
    {
        public FullSubCattegoryViewModel()
        {
            this.Threads = new HashSet<ForumThreadDisplayViewModel>();
        }
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public ICollection<ForumThreadDisplayViewModel> Threads { get; set; }

        public string? ImageUrl { get; set; }

        public string Cattegory { get; set; } = null!;

        public int Id { get; set; }

        public string Slug { get; set; } = null!;

        public int TotalCount { get; set; }
        public PageViewModel Paging { get; set; } = null!;
        
    }
}
