using Ganss.Xss;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu_Gi_Oh_website.Services.Forum.Models
{
    public class PostDto
    {
        public string Author { get; set; } = null!;
              
        public string PostContent { get; set; } = null!;

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.PostContent);

        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public bool? Status { get; set; }

        public string CreatedOn { get; set; } = null!;

        
    }
}
