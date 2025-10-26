namespace OracleBlazor.Core.Entities
{
    public class IEntity
    {
        public IEntity()
        {
            Id = Guid.NewGuid();
        }
       public  Guid Id { get; set; }
        
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToUniversalTime(); 
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.ToUniversalTime(); 

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;

    public bool IsActive { get; set; } = true;
    }
}