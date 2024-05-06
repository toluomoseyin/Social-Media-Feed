using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Services;

namespace SocialMediaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("Post")]

        public async Task<IActionResult> Post(LikePostDTO like)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);

            var (result, statusCode) = await _likeService.AddPostLike(like, userId);

            return StatusCode(statusCode, result);
        }

        [HttpPost("Comment")]

        public async Task<IActionResult> OnComment(CommentLikeDTO comment)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);

            var (result, statusCode) = await _likeService.AddCommentLike(comment, userId);

            return StatusCode(statusCode, result);
        }
    }
}
