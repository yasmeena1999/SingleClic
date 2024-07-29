using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingleClic.Core.DTOs;
using SingleClic.Core.Models;
using SingleClic.Services.IServices;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SingleClic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PostRetrievedDto>>> GetAll()
        {
            var response = await _postService.GetAllPostsAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] BlogPostDto request)
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                return Unauthorized("Unauthorized access.");
            }

            var response = await _postService.CreatePostAsync(request, userId);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                return Unauthorized("Unauthorized access.");
            }

            var response = await _postService.DeletePostAsync(id, userId);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var response = await _postService.GetPostByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }

        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdatePost(int postId, BlogPostDto updatedPost)
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                return Unauthorized("Unauthorized access.");
            }

            var response = await _postService.UpdatePostAsync(postId, updatedPost, userId);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] string title, [FromQuery] string authorName)
        {
            var response = await _postService.GetPostsAsync(title, authorName);
            return Ok(response);
        }
    }
}
