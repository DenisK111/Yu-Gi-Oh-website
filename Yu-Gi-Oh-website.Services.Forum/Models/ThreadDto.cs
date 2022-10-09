using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.Forum.Models;

namespace Yu_Gi_Oh_website.Services.Forum.Models
{
    public class ThreadDto
    {
        
        public ThreadDto()
        {
            Posts = new HashSet<PostDto>();

        }

        public int Id { get; set; }
    
        public string Subject { get; set; } = null!;

        public ICollection<PostDto> Posts { get; set; }

        public string Author { get; set; } = null!;

        public string AuthorId { get; set; } = null!;

        public bool? Status { get; set; }

        public string SubCattegory { get; set; } = null!;
        public int SubCattegoryId { get; set; }
        
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        
        public string Slug { get; set; } = null!;
    }
}
