using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
  public class Student
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Student name is required.")]
    [StringLength(100)]
    [Display(Name = "Student Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
    public int Age { get; set; }

    [Display(Name = "Department")]
    public int? DepartmentId { get; set; }

    public Department? Department { get; set; }
  }
}
