namespace OracleBlazor.Application.DTOs
{
    public record LoginDto(string Username, string Password);
    public record CreateDto(string Username, string Password,string RealName);
    public record LoginResponseDto
    {
        public string Token { get; set; }
    }
}