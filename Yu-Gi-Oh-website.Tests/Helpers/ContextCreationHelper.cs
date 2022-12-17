using Microsoft.EntityFrameworkCore;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Web.Data;

namespace Yu_Gi_Oh_website.Tests.Helpers
{
    public static class ContextCreationHelper
    {


        public static ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                             .Options;

            return new ApplicationDbContext(options);
        }

        public static ApplicationDbContext CreateContextAndAddCattegory()
        {
            Cattegory testCattegory = new Cattegory()
            {
                Id = TestConstants.Forum.CattegoryId,
                Name = TestConstants.Forum.CattegoryName,
                ImageUrl = TestConstants.Forum.CattegoryImageUrl,
            };
            var context = ContextCreationHelper.CreateContext();
            context.Cattegories.Add(testCattegory);
            context.SaveChanges();
            return context;
        }

        public static ApplicationDbContext AddCattegorySubCattegoryAndThread(this ApplicationDbContext context)
        {
            Cattegory testCattegory = new Cattegory()
            {
                Id = TestConstants.Forum.CattegoryId,
                Name = TestConstants.Forum.CattegoryName,
                ImageUrl = TestConstants.Forum.CattegoryImageUrl,
            };

            SubCattegory subCattegory = new SubCattegory()
            {
                Id = TestConstants.Forum.SubCattegoryId,
                Description = TestConstants.Forum.SubCattegoryDescription,
                Name = TestConstants.Forum.SubCattegoryName,
                Slug = TestConstants.Forum.Slug,
                CattegoryId = TestConstants.Forum.CattegoryId
            };

            var author = new ApplicationUser()
            {
                Id = TestConstants.Forum.UserId
            };

            ForumThread thread = new ForumThread()
            {
                Id = TestConstants.Forum.ThreadId,
                Author = author,
                SubCattegoryId = TestConstants.Forum.SubCattegoryId,
                Subject = TestConstants.Forum.Slug,
                Slug = TestConstants.Forum.ThreadSubject,

            };

            context.Cattegories.Add(testCattegory);
            context.SubCattegories.Add(subCattegory);
            context.Threads.Add(thread);
            context.SaveChanges();
            return context;
        }

        public static ApplicationDbContext AddPost(this ApplicationDbContext context)
        {
            context.Posts.Add(new Post()
            {
                Author = context.Users.Find(TestConstants.Forum.UserId)!,
                Thread = context.Threads.Find(TestConstants.Forum.ThreadId)!,
                Id = TestConstants.Forum.PostId,
                Votes = new List<PostVote>(),
                PostContent = context.PostContents.Find(TestConstants.Forum.PostContentId)!,
            });
            context.SaveChanges();
            return context;
        }
    }
}
