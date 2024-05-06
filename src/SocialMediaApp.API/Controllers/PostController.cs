using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Services;

namespace SocialMediaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }


        [HttpPost]
        public async Task<IActionResult> CreatePost(AddPostDTO addPost)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);

            var (result, statusCode) = await _postService.CreatePost(addPost, userId);

            return StatusCode(statusCode, result);
        }
    }
}
