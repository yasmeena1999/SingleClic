using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingleClic.Core.DTOs;
using SingleClic.Services.IServices;
using SingleClic.Services.Services;

namespace SingleClic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto Request)
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                return Unauthorized("Unauthorized access.");
            }

            var response = await _commentService.CreateComment(Request, userId);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                return Unauthorized("Unauthorized access.");
            }

            var response = await _commentService.DeleteComment(id, userId);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto request)
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                return Unauthorized("Unauthorized access.");
            }

            var response = await _commentService.UpdateComment(request, userId);
            return Ok(response);
        }


    }
}
