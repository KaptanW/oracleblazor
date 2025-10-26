using OracleBlazor.Application.DTOs;
using OracleBlazor.Core.Entities;
using OracleBlazor.Core.Filter;

namespace OracleBlazor.Application.Interfaces;

public interface IAssetService
{
    Task<IEnumerable<Asset>> ListAsync(AssetsFilter? filter);

    Task<Asset?> GetAsync(Guid id);
    Task<Asset> CreateAsync(AssetCreateDto dto);
    Task<bool> UpdateAsync(AssetUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}