using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Services;

namespace SocialMediaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser(AddUserDTO addUserDTO)
        {
            var (result, statuscode) = await _userService.CreateUser(addUserDTO);

            return StatusCode(statuscode, result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var (result, statuscode) = await _userService.Login(login);

            return StatusCode(statuscode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var (result, statuscode) = await _userService.GetAllUser();

            return StatusCode(statuscode, result);
        }
    }
}
