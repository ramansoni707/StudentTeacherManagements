namespace StudentTeacherManagement.Models
{
    public class StudentsTeachersViewModel
    {
        public string Class { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}
