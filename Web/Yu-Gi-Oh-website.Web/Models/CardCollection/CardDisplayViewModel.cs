using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu_Gi_Oh_website.Web.Models.CardCollection
{
    public class CardDisplayViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
    }
}
