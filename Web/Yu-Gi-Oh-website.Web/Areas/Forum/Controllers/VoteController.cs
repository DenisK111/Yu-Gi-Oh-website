using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Web.Areas.Forum.Models;
using Yu_Gi_Oh_website.Web.Extentension;

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
            var userId = this.GetUserId()!;
            var result = await votesService.PostVote(vote.Id, userId, vote.IsUpvote);
            return Ok(new { result = result });
        }
    }
}
