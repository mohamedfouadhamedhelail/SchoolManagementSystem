using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.ViewModels;
using System.Diagnostics;

namespace SchoolManagementSystem.Controllers
{
  public class HomeController : Controller
  {
    private readonly SchoolDbContext _context;

    public HomeController(SchoolDbContext context)
    {
      _context = context;
    }

    public async Task<IActionResult> Index()
    {
      ViewBag.StudentsCount = await _context.Students.CountAsync();
      ViewBag.DepartmentsCount = await _context.Departments.CountAsync();

      return View();
    }

    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel
      {
        RequestId = Activity.Current?.Id
              ?? HttpContext.TraceIdentifier
      });
    }
  }
}
