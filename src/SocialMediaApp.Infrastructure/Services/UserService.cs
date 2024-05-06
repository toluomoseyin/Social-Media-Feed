using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Application.Interfaces.Services;
using SocialMediaApp.Model.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMediaApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<(BaseResponse<string>,int)> CreateUser(AddUserDTO addUserDTO)
        {
            var userByUsername = await _userRepository.GetByUsername(addUserDTO.Username);

            if(userByUsername is not null)
            {
                return (BaseResponse<string>.Failure("User already exists"),400);
            }

            var rowAffected = await _userRepository.Add(new Model.Entities.User
            {
                FirstName = addUserDTO.FirstName,
                LastName = addUserDTO.LastName,
                Username = addUserDTO.Username,
                Password = addUserDTO.Password,
            });

            if(rowAffected > 0)
            {
                return (BaseResponse<string>.Success("User successfully created"), 200);
            }

            return (BaseResponse<string>.Failure("Unable to create user"), 500);
        }


        public async Task<(BaseResponse<LoginResponse>,int)> Login(LoginDTO loginDTO)
        {
            var user = await _userRepository.GetByUsername(loginDTO.Username);
            if(user == null)
            {
                return (BaseResponse<LoginResponse>.Failure("Wrong username or password"), 400);
            }

            // Hashing of password was not added intentionally because the user feature was just for easy testing
            var isPasswordCorrect = user.Password.Equals(loginDTO.Password,StringComparison.OrdinalIgnoreCase);

            if(isPasswordCorrect)
            {
               var token =  GenerateJSONWebToken(user);

                var loginResponse = new LoginResponse
                {
                    Token = token,
                    Type = "Bearer"
                };

                return (BaseResponse<LoginResponse>.Success("Login successful",loginResponse), 200);
            }

            return (BaseResponse<LoginResponse>.Failure("Wrong username or password"), 400);
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Name, userInfo.Username),
            new Claim(JwtRegisteredClaimNames.GivenName, $"{userInfo.FirstName} {userInfo.LastName}"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserId",userInfo.Id.ToString())
             };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<(BaseResponse<List<AllUserResponseDTO>>, int)> GetAllUser()
        {
            var usersToReturn = new List<AllUserResponseDTO>();

            var userList = await _userRepository.GetAll();

            foreach (var user in userList)
            {
                usersToReturn.Add(new AllUserResponseDTO()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Id = user.Id,
                });
            }
            return (BaseResponse<List<AllUserResponseDTO>>.Success("Successful", usersToReturn), 200);
        }


    }
}
