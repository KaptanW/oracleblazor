namespace OracleBlazor.Core.Entities
{
   public class Asset : IEntity
    {
         public string Tag { get; set; } = default!;
        public string Name { get; set; } = default!;

    public AssetCategory? Category { get; set; } = default!;
        public AssetLocation? Location { get; set; } = default!;
        public AssetStatus? Status { get; set; } = default!;

    public DateTime? PurchaseDate { get; set; }
    public decimal? Cost { get; set; }
    }

}