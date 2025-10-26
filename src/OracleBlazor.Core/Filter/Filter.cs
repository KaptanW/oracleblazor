using OracleBlazor.Core.Entities;

namespace OracleBlazor.Core.Filter
{
    public record AssetsFilter
{
    public string? Name { get; set; }
    public AssetCategory? Category { get; set; }
    public AssetLocation? Location { get; set; }
    public AssetStatus? Status { get; set; }
    public Pagination Pagination { get; set; } = new Pagination();
}



}