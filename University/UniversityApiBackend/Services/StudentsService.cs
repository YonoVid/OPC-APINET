using UniversityApiBackend.Model.DataModels;

namespace UniversityApiBackend.Services
{
    public class StudentsService : IStudentsService
    {
        public IEnumerable<Course> GetAllCoursesOfStudent(IEnumerable<Student> students, int id)
        {
            Student? student = students.FirstOrDefault(student => student.Id == id);
            if (student != null)
                return student.Courses;
            
            return new List<Course>();
        }

        public IEnumerable<Student> GetStudentsWithCourses(IEnumerable<Student> students)
        {
            return from student in students
                   where student.Courses.Count() > 0
                   select student;
        }

        public IEnumerable<Student> GetStudentsWithNoCourses(IEnumerable<Student> students)
        {
            return from student in students
                   where student.Courses.Count() == 0
                   select student;
        }
    }
}
