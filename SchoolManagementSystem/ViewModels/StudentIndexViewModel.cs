using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.ViewModels
{
  public class StudentIndexViewModel
  {
    public List<Student> Students { get; set; } = new();

    public List<Department> Departments { get; set; } = new();

    public string? SearchTerm { get; set; }

    public int? DepartmentId { get; set; }

    public int TotalStudents { get; set; }

    public int CurrentPage { get; set; } = 1;

    public int TotalPages { get; set; }

    public int PageSize { get; set; } = 10;
  }
}
