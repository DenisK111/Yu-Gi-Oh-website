using System.ComponentModel.DataAnnotations;
using Yu_Gi_Oh_website.Models.BaseModels;
using Yu_Gi_Oh_website.Common;

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

        public string? ImageUrl { get; set; }

        public Cattegory Cattegory { get; set; } = null!;
        public int CattegoryId { get; set; }

        public DateTime? LastThreadModifiedOn { get; set; }
        public string? LastThreadName { get; set; }
        public string? LastPostAuthor { get; set; }

        [MaxLength(100)]
        public string Slug { get; set; } = null!;



    }
}