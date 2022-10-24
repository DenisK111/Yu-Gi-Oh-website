using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Web.Areas.Forum.Models;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Controllers
{
    [Authorize]
    public class VoteController : Controller
    {
        private readonly IVotesService votesService;

        public VoteController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]

        public async Task<IActionResult> ThreadVote(VoteInputViewModel vote)
        {
            var userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await votesService.AddThreadVote(vote.Id, userId, vote.IsUpvote);
            return this.Json(result);
        }
    }
}
