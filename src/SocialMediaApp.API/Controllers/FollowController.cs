using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Services;

namespace SocialMediaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }


        [HttpPost]

        public async Task<IActionResult> Index(FollowDTO followDTO)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);

            var (result, statusCode) = await _followService.AddFollow(followDTO, userId);

            return StatusCode(statusCode, result);
        }
    }
}
