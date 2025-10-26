using OracleBlazor.Application.Interfaces;
using OracleBlazor.Core.Entities;
using OracleBlazor.Infrastructure.Persistence;

namespace OracleBlazor.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
     private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

    public async Task<User> CreateAsync(string userName, string password,string realName)
    {
        var user = new User()
        {
            UserName = userName,
            Password = password,
            RealName=realName
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> LoginAsync(string userName, string password)
    {
        var user = _context.Users.Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
        if (user == null) throw new Exception("User not found");
        return user;
    }
}