using OracleBlazor.Core.Entities;
namespace OracleBlazor.Client.DTOs
{

    public record AssetCreateDto(
        string Tag,
        string Name,
        AssetCategory? Category,
        AssetStatus? Status,
        AssetLocation? Location,
        DateTime? PurchaseDate,
        decimal? Cost);

    public record AssetUpdateDto(
    Guid Id,
        string Tag,
        string Name,
               AssetCategory? Category,
        AssetStatus? Status,
        AssetLocation? Location,
        DateTime? PurchaseDate,
        decimal? Cost);
}