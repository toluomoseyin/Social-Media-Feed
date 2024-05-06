using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<int> Add(User user);
        Task<User> GetByUsername(string username);
        Task<List<User>> GetAll();
        Task<User> GetById(int id);

    }
}
