using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
  public class Department
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Department name is required.")]
    [StringLength(100)]
    [Display(Name = "Department Name")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Student> Students { get; set; }
        = new List<Student>();
  }
}
