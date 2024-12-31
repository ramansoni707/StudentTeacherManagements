using System.ComponentModel.DataAnnotations;

namespace StudentTeacherManagement.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? ImagePath { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Sex { get; set; }

        // Store the classes as a comma-separated string
        [Required]
        public string Classes { get; set; }
        // Navigation Property
        public ICollection<TeacherSubject>? TeacherSubjects { get; set; }
    }
}
