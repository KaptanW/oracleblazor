using OracleBlazor.Application.Interfaces;
using OracleBlazor.Infrastructure.Persistence;
using OracleBlazor.Core.Entities;
using OracleBlazor.Core.Filter;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;

namespace OracleBlazor.Infrastructure.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AppDbContext _context;
        public AssetRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Asset entity) => await _context.Assets.AddAsync(entity);
        

        public Task<bool> DeleteAsync(Asset entity)
        {
            _context.Assets.Remove(entity);
            return Task.FromResult(true);
        }


        public Task<Asset?> GetAsync(Guid id)
        {
             return _context.Assets
                   .AsNoTracking()
                   .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Asset>> Query(AssetsFilter? filter)
        {
            var list = _context.Assets
            .WhereIf(filter?.Name, x => x.Name.ToUpper().Contains(filter!.Name!.Trim().ToUpper()))
            .WhereIf(filter?.Category, x => x.Category == filter.Category)
            .WhereIf(filter?.Location, x => x.Location == filter.Location)
            .WhereIf(filter?.Status, x => x.Status == filter.Status)
            .Page(filter?.Pagination).AsNoTracking().ToList();

            var notDeleted = list.Where(x=>x.IsDeleted == false);
            return notDeleted;
        }

        public async Task UpdateAsync(Asset entity) =>  _context.Assets.Update(entity);
    }
}