using Domain.Common.Entities;

namespace Domain.Entities;

public class Student : BaseEntity
{
    public string StudentID { get; set; }
    public string StudentName { get; set; }
    public string  FathersName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string PostalAddress { get; set; }
    public Guid DepartmentId { get; set; }
    public virtual Department Department { get; set; }

}
