using System.ComponentModel.DataAnnotations;
using Yu_Gi_Oh_website.Models.BaseModels;

namespace Yu_Gi_Oh_website.Models.Forum.Models
{
    public class SubCattegory : BaseDeletableModel<int>
    {
        public SubCattegory()
        {
            Threads = new HashSet<ForumThread>();
        }

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(300)]
        public string Description { get; set; } = null!;

        public ICollection<ForumThread> Threads { get; set; }

        public string ImageUrl { get; set; } = null!;

        public Cattegory Cattegory { get; set; } = null!;
        public int CattegoryId { get; set; }



    }
}