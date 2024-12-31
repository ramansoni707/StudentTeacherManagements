namespace StudentTeacherManagement.Models
{
    public class StudentsClassViewModel
    {
        //public string Class { get; set; }
        //public List<Student> Students { get; set; }
        public string Class { get; set; }
        //public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }
    }

}
