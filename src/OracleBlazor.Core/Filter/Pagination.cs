namespace OracleBlazor.Core.Filter;
public record Pagination
{

    public int No { get; set; } = 1;
    public int Size { get; set; } = 50;
    public string OrderBy { get; set; } = "CreatedAt";
    public string OrderDirection { get; set; } = "desc";


}