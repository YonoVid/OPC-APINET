using UniversityApiBackend.Model.DataModels;

namespace UniversityApiBackend.Services
{
    public interface IStudentsService
    {
        public IEnumerable<Course> GetAllCoursesOfStudent(IEnumerable<Student> students, int id);
        public IEnumerable<Student> GetStudentsWithCourses(IEnumerable<Student> students);
        public IEnumerable<Student> GetStudentsWithNoCourses(IEnumerable<Student> students);
    }
}
