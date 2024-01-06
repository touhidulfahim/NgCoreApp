namespace Domain.Common.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public Guid CreatedById { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public Guid? ModifiedById { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; } = false;
    public Guid? DeletedById { get; set; }
    public DateTime? DeletedDate { get; set; }
}
