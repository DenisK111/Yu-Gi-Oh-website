using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu_Gi_Oh_website.Services.Forum.Models
{
	public class UserInfoDto
	{
		public string Id { get; set; } = null!;

		public string UserName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public int PostCount { get; set; }

		public string ProfilePic { get; set; } = null!;

		public IEnumerable<string> Roles { get; set; } = null!;
	}
}
