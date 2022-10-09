using System.ComponentModel.DataAnnotations;
using Yu_Gi_Oh_website.Models.BaseModels;

namespace Yu_Gi_Oh_website.Models.Forum.Models
{
    public class PostContent : BaseDeletableModel<int>
    {
        [MaxLength(6000)]
        public string Content { get; set; } = null!;
    }
}