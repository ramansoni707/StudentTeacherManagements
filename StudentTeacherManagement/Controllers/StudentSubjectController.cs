using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTeacherManagement.Data;
using StudentTeacherManagement.Models;

namespace StudentTeacherManagement.Controllers
{
    public class StudentSubjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentSubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: List all students with their subjects and teachers
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                    .ThenInclude(s => s.TeacherSubjects)
                    .ThenInclude(ts => ts.Teacher)
                .ToListAsync();

            var groupedStudents = students
                .GroupBy(s => s.Class)
                .ToList();

            return View(groupedStudents);
        }

    }
}
 