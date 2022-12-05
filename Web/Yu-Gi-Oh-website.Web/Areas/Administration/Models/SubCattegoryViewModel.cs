using System.ComponentModel.DataAnnotations;

namespace Yu_Gi_Oh_website.Web.Areas.Administration.Models
{
	public class SubCattegoryViewModel
	{
        public int Id { get; set; }
        [MaxLength(40,ErrorMessage = "Name must be at least 2 characters and maximum 40 characters long.")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters and maximum 40 characters long.")]
        public string Name { get; set; } = null!;
        [MaxLength(200, ErrorMessage = "Name must be at least 15 characters and maximum 200 characters long.")]
        [MinLength(15, ErrorMessage = "Name must be at least 15 characters and maximum 200 characters long.")]
        public string Description { get; set; } = null!;
        
        public int CattegoryId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
