using System.ComponentModel.DataAnnotations;

namespace StudentTeacherManagement.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Class { get; set; }
        public string Languages { get; set; }

        public ICollection<TeacherSubject>? TeacherSubjects { get; set; }
        public ICollection<StudentSubject>? StudentSubjects { get; set; }
    }
}
