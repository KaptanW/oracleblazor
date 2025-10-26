using OracleBlazor.Core.Entities;

namespace OracleBlazor.Application.Interfaces;

public interface IAllowedIpsRepository
{
    Task<IEnumerable<IP>> GetAllowedIpsAsync();
    Task<bool> IsAllowedIpAsync(string ip);
    public Task AddAsync(string ip);
}
