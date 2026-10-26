using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
  public class DepartmentController : Controller
  {
    private readonly SchoolDbContext _context;

    public DepartmentController(SchoolDbContext context)
    {
      _context = context;
    }

    // GET: /Department
    public async Task<IActionResult> Index()
    {
      var departments = await _context.Departments
          .Include(d => d.Students)
          .OrderBy(d => d.Name)
          .ToListAsync();

      return View(departments);
    }

    // GET: /Department/Add
    [HttpGet]
    public IActionResult Add()
    {
      return View();
    }

    // POST: /Department/Add
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(Department department)
    {
      if (ModelState.IsValid)
      {
        bool exists = await _context.Departments
            .AnyAsync(d => d.Name == department.Name);

        if (exists)
        {
          ModelState.AddModelError(
              nameof(department.Name),
              "This department already exists.");
        }
      }

      if (!ModelState.IsValid)
      {
        return View(department);
      }

      _context.Departments.Add(department);
      await _context.SaveChangesAsync();

      TempData["Success"] = "Department added successfully.";

      return RedirectToAction(nameof(Index));
    }

    // GET: /Department/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var department = await _context.Departments.FindAsync(id);

      if (department == null)
      {
        return NotFound();
      }

      return View(department);
    }

    // POST: /Department/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Name")] Department department)
    {
      if (id != department.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        bool exists = await _context.Departments
            .AnyAsync(d =>
                d.Name == department.Name &&
                d.Id != department.Id);

        if (exists)
        {
          ModelState.AddModelError(
              nameof(department.Name),
              "Another department already has this name.");
        }
      }

      if (!ModelState.IsValid)
      {
        return View(department);
      }

      var existingDepartment = await _context.Departments
          .FindAsync(id);

      if (existingDepartment == null)
      {
        return NotFound();
      }

      existingDepartment.Name = department.Name;

      await _context.SaveChangesAsync();

      TempData["Success"] = "Department updated successfully.";

      return RedirectToAction(nameof(Index));
    }

    // GET: /Department/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var department = await _context.Departments
          .Include(d => d.Students)
          .FirstOrDefaultAsync(d => d.Id == id);

      if (department == null)
      {
        return NotFound();
      }

      return View(department);
    }

    // POST: /Department/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var department = await _context.Departments
          .Include(d => d.Students)
          .FirstOrDefaultAsync(d => d.Id == id);

      if (department == null)
      {
        return NotFound();
      }

      if (department.Students.Any())
      {
        TempData["Error"] =
            "Cannot delete a department that still has students.";

        return RedirectToAction(nameof(Index));
      }

      _context.Departments.Remove(department);

      await _context.SaveChangesAsync();

      TempData["Success"] = "Department deleted successfully.";

      return RedirectToAction(nameof(Index));
    }
  }
}
