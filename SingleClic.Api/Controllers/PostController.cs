using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingleClic.Core.DTOs;
using SingleClic.Core.Models;
using SingleClic.Services.IServices;
using System.Security.Claims;

namespace SingleClic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService )
        {
            _postService = postService;
        }
        [HttpPost]
        public  async Task<IActionResult> CreatePost([FromBody] BlogPostDto request)
        {

            

            var response = await _postService.CreatePostAsync(request, User);

            return Ok( response);
        }
    }
}
