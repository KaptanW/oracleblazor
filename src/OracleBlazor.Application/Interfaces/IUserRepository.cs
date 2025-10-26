using OracleBlazor.Core.Entities;

namespace OracleBlazor.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> LoginAsync(string userName,string password);
        Task<User> CreateAsync(string userName,string password,string realName);
    }
}