using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Model.DataModels;

namespace UniversityApiBackend.Services
{
    public interface IStudentsService
    {
        public IEnumerable<Course> GetAllCoursesOfStudent(IEnumerable<Student> students, int id);
        public IEnumerable<Student> GetStudentsWithCourses(IEnumerable<Student> students);
        public IEnumerable<Student> GetStudentsWithNoCourses(IEnumerable<Student> students);
        public Task<List<Course>> AsyncGetAllCoursesOfStudent(IQueryable<Student>? students, int id);
        public Task<List<Student>> AsyncGetStudentsWithCourses(IQueryable<Student> students);
        public Task<List<Student>> AsyncGetStudentsWithNoCourses(IQueryable<Student> students);
    }
}
