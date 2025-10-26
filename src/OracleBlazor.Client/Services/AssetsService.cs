using System.Net.Http.Json;
using OracleBlazor.Client.DTOs;
using OracleBlazor.Core.Entities;
using OracleBlazor.Core.Filter;
namespace OracleBlazor.Client.Services
{
    public class AssetsService
    {
        private readonly HttpClient _http;
        public AssetsService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("Api");
        }

        public async Task<List<Asset>> FetchAssets(AssetsFilter filter,CancellationToken ct = default)
        {
            var res = await _http.PostAsJsonAsync("assets/list", filter,ct);
            if (!res.IsSuccessStatusCode) return new List<Asset>();
            return await res.Content.ReadFromJsonAsync<List<Asset>>() ?? new List<Asset>();
        }
        public async Task<bool> DeleteAsset(Guid id, CancellationToken ct = default)
        {
            var res = await _http.DeleteAsync("assets/" + id.ToString(), ct);
            if (!res.IsSuccessStatusCode) return false;
            return true;
        }
        
        public async Task CreateAsset(AssetCreateDto dto, CancellationToken ct = default)
        {
            var res = await _http.PostAsJsonAsync("assets", dto, ct);
            if (!res.IsSuccessStatusCode) throw new Exception("Asset can not created");
            
        }
        public async Task UpdateAsset(AssetUpdateDto dto, CancellationToken ct = default)
        {
            var res = await _http.PutAsJsonAsync("assets", dto, ct);
            if (!res.IsSuccessStatusCode) throw new Exception("Asset can not updated");
            
        }
    }
}