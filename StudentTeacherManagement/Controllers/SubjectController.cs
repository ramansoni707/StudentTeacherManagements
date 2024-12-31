using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTeacherManagement.Data;
using StudentTeacherManagement.Models;

namespace StudentTeacherManagement.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Subjects
        public IActionResult Index()
        {
            var subjects = _context.Subjects.ToList();
            return View(subjects);
        }


        // GET: Subject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Subject subject, string languages)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(languages))
                {
                    ModelState.AddModelError("Languages", "Languages cannot be empty.");
                    return View(subject);
                }

                subject.Languages = languages;

                _context.Subjects.Add(subject);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(subject);
        }


       
    }
}
