using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Application.Interfaces.Services;

namespace SocialMediaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedController : ControllerBase
    {
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService)
        {
            _feedService = feedService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);

            var (result, statuscode) = await _feedService.GetUserFeed(userId, pageSize, pageNumber);

            return StatusCode(statuscode, result);
        }
    }
}
