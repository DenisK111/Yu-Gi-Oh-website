using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Web.Areas.Forum.Models;
using Yu_Gi_Oh_website.Web.Extentension;
using Yu_Gi_Oh_website.Web.Helpers;
using Yu_Gi_Oh_website.Web.Models.Contracts;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    [Route("{area}/Cattegory/{subCattegoryId:int}/{subCattegorySlug}/Thread/")]
    public class ThreadController : Controller
    {
        private readonly int forumPostsToTake = 15;
        private readonly IThreadService threadService;
        private readonly IMapper mapper;
        private readonly IVotesService voteService;
        private readonly IVisitorCountService visitorCountService;

        public ThreadController(IThreadService threadService, IMapper mapper, IVotesService voteService,IVisitorCountService visitorCountService)
        {
            this.threadService = threadService;
            this.mapper = mapper;
            this.voteService = voteService;
            this.visitorCountService = visitorCountService;
        }
        [Route("{id:int}")]

        public async Task<IActionResult> Thread(int id, int currentPage)
        {
            if (!ModelState.IsValid)
            {
                return this.View("error404");
            }
          
            var path = this.HttpContext.Request.Path;
            var ip = this.HttpContext.Connection.RemoteIpAddress!.ToString();

            await visitorCountService.AddOrUpdateAsync(path, ip,id);

            currentPage = Paging.PageCheck(currentPage);
          
            var threadDetails = await threadService.GetThreadDtoById(id, forumPostsToTake, currentPage - 1);

            var model = mapper.Map<ThreadViewModel>(threadDetails.thread);

            Paging.CreatePaging(model, threadDetails.postCount, forumPostsToTake, currentPage);            

            var userEmail = this.GetUserEmail();
            var userId = this.GetUserId();

            if (userId != null)
            {
                foreach (var post in model.Posts.Where(x => x.Author == userEmail))
                {
                    var vote = await voteService.GetVoteByUserIdPostId(userId, post.Id);
                    if (vote is null)
                    {
                        continue;
                    }

                    post.IsVoted = true;
                    post.IsUpvote = vote.IsUpvote;
                }
            }

            return this.View(model);
        }

    }
}
