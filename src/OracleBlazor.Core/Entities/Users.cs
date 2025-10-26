namespace OracleBlazor.Core.Entities
{
    public class User:IEntity
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string RealName { get; set; } = default!;
    }
}