using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.BaseModels;

namespace Yu_Gi_Oh_website.Models.Forum.Models
{
    public class Cattegory : BaseDeletableModel<int>
    {
        public Cattegory()
        {
            SubCattegories = new HashSet<SubCattegory>();
        }

        [MaxLength(60)]
        public string Name { get; set; } = null!;

        public ICollection<SubCattegory> SubCattegories { get; set; }

        public string? ImageUrl { get; set; }


    }
}
