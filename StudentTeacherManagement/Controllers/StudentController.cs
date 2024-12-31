using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTeacherManagement.Data;
using StudentTeacherManagement.Models;

namespace StudentTeacherManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<StudentController> _logger;
        private readonly IWebHostEnvironment _env;
        public StudentController(ApplicationDbContext context, ILogger<StudentController> logger,IWebHostEnvironment env)
        
        {
            _context = context;
            _logger = logger;
            _env = env;

        }

        // GET: Students
        public IActionResult Index()
        {
            _logger.LogInformation("Fetching all students from the database.");
            var students = _context.Students.ToList();
            _logger.LogInformation("Successfully retrieved {StudentCount} students.", students.Count);
            return View(students);
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Navigating to Create Student page.");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student, IFormFile? image)
        {
            // Check if a student with the same roll number already exists in the same class
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(s => s.Class == student.Class && s.RollNumber == student.RollNumber);

            if (existingStudent != null)
            {
                // If a student with the same class and roll number exists, add an error to ModelState
                ModelState.AddModelError("RollNumber", "A student with this roll number already exists in this class.");
            }

            // Check if the image is present and save it if available
            if (image != null && image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/studentimage", fileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                // Save the file to the specified location
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // Set the ImagePath for the database
                student.ImagePath = "studentimage/" + fileName;
            }

            // After handling the image and validation, check if ModelState is valid
            if (ModelState.IsValid)
            {
                // Save the student data if valid
                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Student with ID {StudentId} successfully added.", student.Id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Log errors for debugging
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }
            }

            _logger.LogWarning("ModelState is invalid. Student creation failed.");
            return View(student);
        }

    }
}
 
