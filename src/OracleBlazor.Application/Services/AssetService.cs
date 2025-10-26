using OracleBlazor.Application.DTOs;
using OracleBlazor.Application.Interfaces;
using OracleBlazor.Core.Entities;
using OracleBlazor.Core.Filter;

namespace OracleBlazor.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _repo;
        private readonly IUnitOfWork _uow;

        public AssetService(IAssetRepository repo, IUnitOfWork uow)
        { _repo = repo; _uow = uow; }
        public async Task<Asset> CreateAsync(AssetCreateDto dto)
        {
            var Asset = new Asset()
            {
                Tag = dto.Tag,
                Name = dto.Name,
                Category = dto.Category,
                Location = dto.Location,
                Status = dto.Status,
                PurchaseDate = dto.PurchaseDate,
                Cost = dto.Cost,
            };
            await _repo.AddAsync(Asset);
             await _uow.SaveChangesAsync();
            return Asset;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
                    var Asset = await _repo.GetAsync(id);
            if (Asset is null) return false;
            bool deleted = await _repo.DeleteAsync(Asset);
            if(!deleted) return false;
            await  _uow.SaveChangesAsync();
            return true;
        }

        public Task<Asset?> GetAsync(Guid id)
        {
            return _repo.GetAsync(id);
        }

        public async Task<IEnumerable<Asset>> ListAsync(AssetsFilter? filter)
        {

            return  await _repo.Query(filter);
            
        }

        public async Task<bool> UpdateAsync(AssetUpdateDto dto)
        {
            var Asset = await _repo.GetAsync(dto.Id);
            if (Asset is null) return false;
            Asset.Tag = dto.Tag;
            Asset.Name = dto.Name;
            Asset.Category = dto.Category;
            Asset.Location = dto.Location;
            Asset.Status = dto.Status;
            Asset.PurchaseDate = dto.PurchaseDate;
            Asset.Cost = dto.Cost;
            await _repo.UpdateAsync(Asset);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}