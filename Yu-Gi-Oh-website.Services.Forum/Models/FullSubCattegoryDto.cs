using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu_Gi_Oh_website.Services.Forum.Models
{
    public class FullSubCattegoryDto
    {
        public FullSubCattegoryDto()
        {
            this.Threads = new HashSet<ForumThreadDisplayDto>();
        }
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public ICollection<ForumThreadDisplayDto> Threads { get; set; }

        public string? ImageUrl { get; set; }

        public string Cattegory { get; set; } = null!;

        public int Id { get; set; }

        public string Slug { get; set; } = null!;




    }
}
