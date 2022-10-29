using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Web.Areas.Forum.Models;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]    
    public class VoteController : ControllerBase
    {
        private readonly IVotesService votesService;

        public VoteController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostVote([FromBody] VoteInputViewModel vote)
        {
            var userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await votesService.PostVote(vote.Id, userId, vote.IsUpvote);
            return Ok(new { result = result });
        }
    }
}
