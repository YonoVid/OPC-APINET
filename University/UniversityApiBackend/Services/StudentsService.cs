using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Model.DataModels;

namespace UniversityApiBackend.Services
{
    public class StudentsService : IStudentsService
    {
        public async Task<List<Course>> AsyncGetAllCoursesOfStudent(IQueryable<Student> students, int id)
        {
            Student? student = await students.FirstOrDefaultAsync(student => student.Id == id);
            if (student != null)
                return student.Courses.ToList();

            return new List<Course>();
        }

        public async Task<List<Student>> AsyncGetStudentsWithCourses(IQueryable<Student> students)
            => await (from student in students where student.Courses.Count() > 0 select student).ToListAsync();

        public async Task<List<Student>> AsyncGetStudentsWithNoCourses(IQueryable<Student> students)
            => await (from student in students where student.Courses.Count() == 0 select student).ToListAsync();

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

