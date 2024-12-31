using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTeacherManagement.Data;
using StudentTeacherManagement.Models;
using System.Diagnostics;
using System.Linq;

namespace StudentTeacherManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index(string searchName)
        {
            var studentsQuery = _context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {
                studentsQuery = studentsQuery.Where(s => s.Name.Contains(searchName));
            }

            var studentsByClass = studentsQuery
                .GroupBy(s => s.Class)
                .Select(g => new StudentsClassViewModel
                {
                    Class = g.Key,
                    Students = g.ToList()
                })
                .ToList();

            ViewData["SearchName"] = searchName; // Pass search name to the view
            return View(studentsByClass);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
