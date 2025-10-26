namespace OracleBlazor.Core.Auth
{
   public  class JwtSettings
{
    public string Key { get; init; } = default!;
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public int ExpireMinutes { get; init; }
}

}