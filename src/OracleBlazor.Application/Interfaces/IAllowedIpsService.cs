using OracleBlazor.Application.DTOs;

namespace OracleBlazor.Application.Interfaces
{
    public interface IAllowedIpsService
    {
        Task CreateAllowedIp(CreateAllowedIpDto dto);
    }
}
