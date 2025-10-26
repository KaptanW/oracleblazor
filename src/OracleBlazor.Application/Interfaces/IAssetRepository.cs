using OracleBlazor.Core.Entities;
using OracleBlazor.Core.Filter;

namespace OracleBlazor.Application.Interfaces
{
    public interface IAssetRepository
{
    Task<IEnumerable<Asset>> Query(AssetsFilter? filter);
    Task<Asset?> GetAsync(Guid id);
    Task AddAsync(Asset entity);
    Task UpdateAsync(Asset entity);
    Task<bool> DeleteAsync(Asset entity);
}
}