using Domain.Common.Entities;

namespace Domain.Entities;

public class Department : BaseEntity
{
    public string DepartmentCode { get; set; }
    public string DepartmentName { get; set; }

    public virtual ICollection<Student> StudentList { get; set; }
}
