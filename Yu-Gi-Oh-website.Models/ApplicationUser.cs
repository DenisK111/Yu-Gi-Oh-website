using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.BaseModels;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;
using Yu_Gi_Oh_website.Models.Forum.Models;

namespace Yu_Gi_Oh_website.Models
{
    

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
           
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.FavouriteCards = new HashSet<Card>();
            this.Posts = new HashSet<Post>();
            this.Threads = new HashSet<ForumThread>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public string? ProfilePic { get; set; }

        public int? PostsCount { get; set; }

        public ICollection<Card> FavouriteCards { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<ForumThread> Threads { get; set; }

        public ICollection<ThreadVote> ThreadVotes { get; set; }
        public ICollection<PostVote> PostVotes { get; set; }
    }
}
