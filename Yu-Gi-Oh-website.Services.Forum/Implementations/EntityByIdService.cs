using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Web.Data;
using Microsoft.EntityFrameworkCore;
using Yu_Gi_Oh_website.Services.Forum.Contracts;

namespace Yu_Gi_Oh_website.Services.Forum.Implementations
{
    public class EntityByIdService : IEntityByIdService
    {
        private readonly ApplicationDbContext context;

        public EntityByIdService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ForumThread?> GetThreadById(int threadId)
        {
            var thread = await context.Threads
                .Include(x => x.Author)
                .Include(x => x.SubCattegory)
                .Include(x => x.Posts)
                .ThenInclude(x => x.PostContent)
                .FirstOrDefaultAsync(x => x.Id == threadId);

            return thread;
        }

        public async Task<ApplicationUser?> GetAuthorByUserName(string userName)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
