namespace SocialMediaApp.Application.Interfaces.Repositories
{
    using System.Threading.Tasks;

    public interface IRedisRepository
    {
        Task<string> GetValueAsync(string key);
        Task SetValueAsync(string key, string value);
        Task<bool> KeyExistsAsync(string key);
        Task<bool> DeleteKeyAsync(string key);
    }

}
