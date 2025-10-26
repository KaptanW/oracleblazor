using OracleBlazor.Application.Interfaces;
using OracleBlazor.Core.Entities;
using OracleBlazor.Infrastructure.Persistence;

namespace OracleBlazor.Infrastructure.Repositories;

public class AllowedIpsRepository : IAllowedIpsRepository
{
        private readonly AppDbContext _context;

    public AllowedIpsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IP>> GetAllowedIpsAsync()
    {
        return  _context.AllowedIps.ToList();
    }

    public async Task<bool> IsAllowedIpAsync(string ip)
    {
        return _context.AllowedIps.Any(x => x.Ip == ip);
    }
    public async Task AddAsync(string ip)
    {
        var entity = new IP() { Ip = ip }; await _context.AllowedIps.AddAsync(entity);
    }
}