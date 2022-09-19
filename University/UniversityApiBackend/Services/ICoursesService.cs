using UniversityApiBackend.Model.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICoursesService
    {
        public IEnumerable<Course> GetWithoutChapters(IEnumerable<Course> courses);
        public IEnumerable<Chapters> GetAllChaptersInCourse(IEnumerable<Course> courses, int id);
        public IEnumerable<Student> GetAllStudentsInCourse(IEnumerable<Course> courses, int id);
    }
}
