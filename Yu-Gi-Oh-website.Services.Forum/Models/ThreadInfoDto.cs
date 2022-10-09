using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu_Gi_Oh_website.Services.Forum.Models
{
    public class ThreadInfoDto
    {
        public int Id { get; set; }

        public int SubCattegoryId { get; set; }

        public string SubCattegoryName { get; set; } = null!;

        public bool IsError { get; set; }

        public string? ErrorMessage { get; set; }

    }
}
