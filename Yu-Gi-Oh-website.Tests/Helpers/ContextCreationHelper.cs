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
                Subject = TestConstants.Forum.ThreadSubject,
                Slug = TestConstants.Forum.Slug,

            };

            context.Cattegories.Add(testCattegory);
            context.SubCattegories.Add(subCattegory);
            context.Threads.Add(thread);
            context.SaveChanges();
            return context;
        }

        public static ApplicationDbContext AddPost(this ApplicationDbContext context)
        {
            var postContent = new PostContent()
            {
                Id = 1,
                Content = TestConstants.Forum.PostContentContent
            };

            context.Posts.Add(new Post()
            {
                Author = context.Users.Find(TestConstants.Forum.UserId)!,
                Thread = context.Threads.Find(TestConstants.Forum.ThreadId)!,
                Id = TestConstants.Forum.PostId,
                Votes = new List<PostVote>(),
                PostContent = postContent,
                ThreadId = TestConstants.Forum.ThreadId,
            });
            context.SaveChanges();
            return context;
        }

        public static ApplicationDbContext AddCattegory(this ApplicationDbContext context)
        {
            context.Cattegories.Add(new Cattegory()
            {
                Id = TestConstants.Forum.CattegoryId,
                Name = TestConstants.Forum.CattegoryName,
                ImageUrl = TestConstants.Forum.CattegoryImageUrl,
            });
            context.SaveChanges();
            return context;
        }

        public static ApplicationDbContext AddSubCattegory(this ApplicationDbContext context)
        {
            context.SubCattegories.Add(new SubCattegory()
            {
                Id = TestConstants.Forum.SubCattegoryId,
                Description = TestConstants.Forum.SubCattegoryDescription,
                Name = TestConstants.Forum.SubCattegoryName,
                Slug = TestConstants.Forum.Slug,
                CattegoryId = TestConstants.Forum.CattegoryId
            });
            context.SaveChanges();
            return context;
        }


        public static ApplicationDbContext AddThread(this ApplicationDbContext context)
        {
            var author = new ApplicationUser()
            {
                Id = TestConstants.Forum.UserId
            };          

            context.Threads.Add(new ForumThread()
            {
                Id = TestConstants.Forum.ThreadId,
                Author = author,
                SubCattegoryId = TestConstants.Forum.SubCattegoryId,
                Subject = TestConstants.Forum.ThreadSubject,
                Slug = TestConstants.Forum.Slug,
                

            });
            context.SaveChanges();
            return context;
        }

        public static ApplicationDbContext AddPostVote(this ApplicationDbContext context,bool isUpvote)
        {
            var postVote = new PostVote()
            {
                Id = TestConstants.Forum.PostVoteId,
                UserId = TestConstants.Forum.UserId,
                IsUpvote = isUpvote,
                PostId = TestConstants.Forum.PostId,
            };

            context.PostVotes.Add(postVote);
            context.SaveChanges();
            return context;
        }

        public static ApplicationDbContext AddDeletedPostVote(this ApplicationDbContext context, bool isUpvote)
        {
            var postVote = new PostVote()
            {
                Id = TestConstants.Forum.PostVoteId,
                UserId = TestConstants.Forum.UserId,
                IsUpvote = isUpvote,
                PostId = TestConstants.Forum.PostId,
                IsDeleted = true
            };

            context.PostVotes.Add(postVote);
            context.SaveChanges();
            return context;
        }
    }
}
