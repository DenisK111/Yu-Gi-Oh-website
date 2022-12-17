using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Models.Forum.Models;

namespace Yu_Gi_Oh_website.Tests
{
    public static class TestConstants
    {
        public static class Forum
        {
            public static int CattegoryId => 1;
            public static int SubCattegoryId => 1;
            public static int ThreadId => 1;
            public static string CattegoryName => "Test Cattegory";
            public static string SubCattegoryName => "testSubCattegory";
            public static string SubCattegoryDescription => "adawd";
            public static string Slug => "slug";
            public static string ThreadSubject => "Namet";
            public static string CattegoryImageUrl => "awdawd";
            public static string UserId => "tests";
            public static int PostId => 1;
            public static int PostContentId => 1;
            public static string PostContentContent => "Test content";

            public static Cattegory Cattegory => new Cattegory()
            {
                Id = TestConstants.Forum.CattegoryId,
                Name = TestConstants.Forum.CattegoryName,
                ImageUrl = TestConstants.Forum.CattegoryImageUrl,
            };

            public static SubCattegory SubCattegory => new SubCattegory()
            {
                Id = TestConstants.Forum.SubCattegoryId,
                Description = TestConstants.Forum.SubCattegoryDescription,
                Name = TestConstants.Forum.SubCattegoryName,
                Slug = TestConstants.Forum.Slug,
                CattegoryId = TestConstants.Forum.CattegoryId
            };

            public static ApplicationUser User => new ApplicationUser()
            {
                Id = TestConstants.Forum.UserId
            };

            public static ForumThread Thread => new ForumThread()
            {
                Id = TestConstants.Forum.ThreadId,
                Author = User,
                SubCattegoryId = TestConstants.Forum.SubCattegoryId,
                Subject = TestConstants.Forum.Slug,
                Slug = TestConstants.Forum.ThreadSubject,
                SubCattegory = SubCattegory

            };

            public static Post Post => new Post()
            {
                Author = User,
                Thread = Thread,
                Id = PostId,
                Votes = new List<PostVote>(),
                PostContent=PostContent,               
            };

            public static PostContent PostContent => new PostContent()
            {
                Id = PostContentId,
                Content = PostContentContent,
            };
        }
    }
}
