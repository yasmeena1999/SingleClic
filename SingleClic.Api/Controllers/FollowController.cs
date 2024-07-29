using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingleClic.Services.IServices;

namespace SingleClic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }
        [HttpPost("follow/{followeeId}")]
        public async Task<IActionResult> FollowUser(string followeeId)
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                return Unauthorized("Unauthorized access.");
            }

            var response = await _followService.FollowUser(userId, followeeId);
            return Ok(response);
        }

        [HttpPost("unfollow/{followeeId}")]
        public async Task<IActionResult> UnfollowUser(string followeeId)
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                return Unauthorized("Unauthorized access.");
            }

            var response = await _followService.UnfollowUser(userId, followeeId);
            return Ok(response);
        }
        [HttpGet("me")]
        public async Task<IActionResult> GetMyFollowersAndFollowings()
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                return Unauthorized("Unauthorized access.");
            }

            var response = await _followService.GetUserWithFollowersAndFollowingsAsync(userId);
            return Ok(response);
        }

    }
}