namespace Application.Common.Models.DTO.Student;

public class StudentResponse
{
    public Guid Id { get; set; }
    public string StudentID { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string FathersName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Mobile { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PostalAddress { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;

}
