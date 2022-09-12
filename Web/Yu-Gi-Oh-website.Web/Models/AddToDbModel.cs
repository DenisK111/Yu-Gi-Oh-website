using System.ComponentModel.DataAnnotations;

namespace Yu_Gi_Oh_website.Web.Models
{
    public class AddToDbModel
    {
        [Required(ErrorMessage ="* Required")]
        [Display(Name ="Card Name")]
        [MinLength(3,ErrorMessage ="* Min Length - 3")]
        public string cardName { get; set; } = null!;
    }
}
