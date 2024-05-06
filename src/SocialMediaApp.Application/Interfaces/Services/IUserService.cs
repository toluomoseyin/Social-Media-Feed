using SocialMediaApp.Application.DTOs;

namespace SocialMediaApp.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<(BaseResponse<string>, int)> CreateUser(AddUserDTO addUserDTO);
        Task<(BaseResponse<LoginResponse>, int)> Login(LoginDTO loginDTO);
        Task<(BaseResponse<List<AllUserResponseDTO>>, int)> GetAllUser();
    }
}
