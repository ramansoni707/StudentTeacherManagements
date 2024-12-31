using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTeacherManagement.Data;
using StudentTeacherManagement.Models;
using System.Linq;

namespace StudentTeacherManagement.Controllers
{
    public class TeacherSubjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherSubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeacherSubject/Index
        public IActionResult Index()
        {
            var teacherSubjects = _context.Teachers
                .Include(t => t.TeacherSubjects)
                    .ThenInclude(ts => ts.Subject)
                .ToList();

            var viewModel = teacherSubjects.Select(t => new TeacherSubjectViewModel
            {
                Teacher = t,
                Subjects = t.TeacherSubjects.Select(ts => ts.Subject)
            }).ToList();

            return View(viewModel);
        }
    }
}
