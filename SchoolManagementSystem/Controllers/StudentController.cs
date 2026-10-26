using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;

namespace SchoolManagementSystem.Controllers
{
  public class StudentController : Controller
  {
    private readonly SchoolDbContext _context;

    public StudentController(SchoolDbContext context)
    {
      _context = context;
    }

    // GET: /Student
    public async Task<IActionResult> Index(
        string? searchTerm,
        int? departmentId,
        int page = 1)
    {
      const int pageSize = 10;

      if (page < 1)
      {
        page = 1;
      }

      var query = _context.Students
          .Include(s => s.Department)
          .AsQueryable();

      if (!string.IsNullOrWhiteSpace(searchTerm))
      {
        string term = searchTerm.Trim();

        query = query.Where(s =>
            s.Name.Contains(term) ||
            s.Email.Contains(term));
      }

      if (departmentId.HasValue)
      {
        query = query.Where(s =>
            s.DepartmentId == departmentId.Value);
      }

      int totalStudents = await query.CountAsync();

      int totalPages = (int)Math.Ceiling(
          totalStudents / (double)pageSize);

      if (totalPages > 0 && page > totalPages)
      {
        page = totalPages;
      }

      var students = await query
          .OrderBy(s => s.Id)
          .Skip((page - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync();

      var viewModel = new StudentIndexViewModel
      {
        Students = students,

        Departments = await _context.Departments
              .OrderBy(d => d.Name)
              .ToListAsync(),

        SearchTerm = searchTerm,
        DepartmentId = departmentId,
        TotalStudents = totalStudents,
        CurrentPage = page,
        TotalPages = totalPages,
        PageSize = pageSize
      };

      return View(viewModel);
    }

    // GET: /Student/Add
    [HttpGet]
    public async Task<IActionResult> Add()
    {
      await LoadDepartmentsAsync();

      return View(new Student());
    }

    // POST: /Student/Add
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(Student student)
    {
      if (ModelState.IsValid)
      {
        bool emailExists = await _context.Students
            .AnyAsync(s => s.Email == student.Email);

        if (emailExists)
        {
          ModelState.AddModelError(
              nameof(student.Email),
              "This email is already registered.");
        }
      }

      if (student.DepartmentId.HasValue)
      {
        bool departmentExists = await _context.Departments
            .AnyAsync(d => d.Id == student.DepartmentId.Value);

        if (!departmentExists)
        {
          ModelState.AddModelError(
              nameof(student.DepartmentId),
              "Please select a valid department.");
        }
      }

      if (!ModelState.IsValid)
      {
        await LoadDepartmentsAsync(student.DepartmentId);

        return View(student);
      }

      _context.Students.Add(student);

      await _context.SaveChangesAsync();

      TempData["Success"] = "Student added successfully.";

      return RedirectToAction(nameof(Index));
    }

    // GET: /Student/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var student = await _context.Students
          .Include(s => s.Department)
          .FirstOrDefaultAsync(s => s.Id == id);

      if (student == null)
      {
        return NotFound();
      }

      return View(student);
    }

    // GET: /Student/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var student = await _context.Students.FindAsync(id);

      if (student == null)
      {
        return NotFound();
      }

      await LoadDepartmentsAsync(student.DepartmentId);

      return View(student);
    }

    // POST: /Student/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Name,Email,Age,DepartmentId")] Student student)
    {
      if (id != student.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        bool emailExists = await _context.Students
            .AnyAsync(s =>
                s.Email == student.Email &&
                s.Id != student.Id);

        if (emailExists)
        {
          ModelState.AddModelError(
              nameof(student.Email),
              "Another student already uses this email.");
        }
      }

      if (student.DepartmentId.HasValue)
      {
        bool departmentExists = await _context.Departments
            .AnyAsync(d => d.Id == student.DepartmentId.Value);

        if (!departmentExists)
        {
          ModelState.AddModelError(
              nameof(student.DepartmentId),
              "Please select a valid department.");
        }
      }

      if (!ModelState.IsValid)
      {
        await LoadDepartmentsAsync(student.DepartmentId);

        return View(student);
      }

      var existingStudent = await _context.Students
          .FindAsync(id);

      if (existingStudent == null)
      {
        return NotFound();
      }

      existingStudent.Name = student.Name;
      existingStudent.Email = student.Email;
      existingStudent.Age = student.Age;
      existingStudent.DepartmentId = student.DepartmentId;

      await _context.SaveChangesAsync();

      TempData["Success"] = "Student updated successfully.";

      return RedirectToAction(nameof(Index));
    }

    // GET: /Student/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var student = await _context.Students
          .Include(s => s.Department)
          .FirstOrDefaultAsync(s => s.Id == id);

      if (student == null)
      {
        return NotFound();
      }

      return View(student);
    }

    // POST: /Student/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var student = await _context.Students.FindAsync(id);

      if (student == null)
      {
        return NotFound();
      }

      _context.Students.Remove(student);

      await _context.SaveChangesAsync();

      TempData["Success"] = "Student deleted successfully.";

      return RedirectToAction(nameof(Index));
    }

    private async Task LoadDepartmentsAsync(int? selectedDepartmentId = null)
    {
      var departments = await _context.Departments
          .OrderBy(d => d.Name)
          .ToListAsync();

      ViewBag.DepartmentId = new SelectList(
          departments,
          nameof(Department.Id),
          nameof(Department.Name),
          selectedDepartmentId);
    }
  }
}
