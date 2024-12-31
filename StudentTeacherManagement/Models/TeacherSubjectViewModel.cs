namespace StudentTeacherManagement.Models
{
    public class TeacherSubjectViewModel
    {
        public Teacher Teacher { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
    }
}
