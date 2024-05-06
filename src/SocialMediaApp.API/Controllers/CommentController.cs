using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Services;
using SocialMediaApp.Infrastructure.Services;

namespace SocialMediaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]

        public async Task<IActionResult> Index(CommentDTO comment)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);

            var (result, statusCode) = await _commentService.AddComment(comment, userId);

            return StatusCode(statusCode, result);
        }

        [HttpPost("OnComment")]

        public async Task<IActionResult> OnComment(CommentOnCommentDTO comment)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);

            var (result, statusCode) = await _commentService.AddCommentOnComment(comment, userId);

            return StatusCode(statusCode, result);
        }
    }
}
