using System.ComponentModel.DataAnnotations;
using Yu_Gi_Oh_website.Common.ValidationAttributes;

namespace Yu_Gi_Oh_website.Web.Models.ProfilePic
{
    public class ProfilePicViewModel
    {

        public string? ProfilePic { get; set; } = null!;
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile NewProfilePic { get; set; } = null!;
    }
}
