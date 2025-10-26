using OracleBlazor.Application.DTOs;
using OracleBlazor.Application.Interfaces;
using OracleBlazor.Core.Entities;
using OracleBlazor.Core.Filter;

namespace OracleBlazor.Application.Services
{
    public class AllowedIpsService : IAllowedIpsService
    {
        private readonly IAllowedIpsRepository _repo;
        private readonly IUnitOfWork _uow;

        public AllowedIpsService(IAllowedIpsRepository repo, IUnitOfWork uow)
        { _repo = repo; _uow = uow; }


        public async Task CreateAllowedIp(CreateAllowedIpDto dto)
        {
            await _repo.AddAsync(dto.IpAddress);
             await _uow.SaveChangesAsync();
        }
    }

}