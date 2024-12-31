using System;
using Microsoft.AspNetCore.Mvc;
using StudentTeacherManagement.Data;
using StudentTeacherManagement.Models;

namespace StudentTeacherManagement.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(ApplicationDbContext context, ILogger<TeacherController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Teachers
        public IActionResult Index()
        {
            var teachers = _context.Teachers.ToList();
            return View(teachers);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher, IFormFile? image)
        {
            if (image != null && image.Length > 0)
            {
                // Generate a unique file name for the image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/teacherimage", fileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                // Save the image to the "teacherimage" folder
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // Set the ImagePath for the teacher
                teacher.ImagePath = "teacherimage/" + fileName;
            }

            // Validate ModelState after handling the image
            if (ModelState.IsValid)
            {
                try
                {
                    // Add teacher to the database
                    _context.Teachers.Add(teacher);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Teacher with ID {TeacherId} successfully added.", teacher.Id);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while saving teacher data.");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data.");
                }
            }
            else
            {
                // Log validation errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }
            }

            _logger.LogWarning("ModelState is invalid. Teacher creation failed.");
            return View(teacher);
        }


    }
}
