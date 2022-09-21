using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Model.DataModels;

namespace UniversityApiBackend.Services
{
    public class CoursesService : ICoursesService
    {
        public async Task<Chapters> AsyncGetAllChaptersInCourse(IQueryable<Course> courses, int id)
        {
            Course? courseFounded = await courses.FirstOrDefaultAsync(course => course.Id == id);

            if (courseFounded != null)
                return courseFounded.Chapters;

            return new Chapters();
        }

        public async Task<List<Student>> AsyncGetAllStudentsInCourse(IQueryable<Course> courses, int id)
        {
            Course? courseFounded = await courses.FirstOrDefaultAsync(course => course.Id == id);

            if (courseFounded != null)
                return (courseFounded.Students).ToList();
            return new List<Student>();
        }

        public async Task<List<Course>> AsyncGetWithoutChapters(IQueryable<Course> courses)
            => await (from course in courses where course.Chapters.List.Count() > 0 select course).ToListAsync();

        public IEnumerable<Chapters> GetAllChaptersInCourse(IEnumerable<Course> courses, int id)
        {
            Course? courseFounded = courses.FirstOrDefault(course => course.Id == id);

            if (courseFounded != null)
                return (IEnumerable<Chapters>)courseFounded.Chapters;

            return new List<Chapters>();
        }

        public IEnumerable<Student> GetAllStudentsInCourse(IEnumerable<Course> courses, int id)
        {
            Course? courseFounded = courses.FirstOrDefault(course => course.Id == id);

            if(courseFounded != null)
                return courseFounded.Students;
            return new List<Student>();
        }

        public IEnumerable<Course> GetWithoutChapters(IEnumerable<Course> courses)
        {
            return from course in courses
                   where course.Chapters.List.Count() > 0
                   select course;
        }
    }
}
