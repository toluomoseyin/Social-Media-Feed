using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Infrastructure.Persistence.Context;
using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Infrastructure.Persistence.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(User user)
        {
            await _appDbContext.Users.AddAsync(user);

            return await _appDbContext.SaveChangesAsync();
        }


        public async Task<User> GetById(int id)
        {
           return  await _appDbContext.Users.FindAsync(id);

         
        }
        public async Task<List<User>> GetAll()
        {
            return await _appDbContext.Users.ToListAsync();

        }

        public async Task<User> GetByUsername(string username)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(x=>x.Username == username);


        }
    }
}
