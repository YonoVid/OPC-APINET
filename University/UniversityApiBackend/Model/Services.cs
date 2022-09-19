using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversityApiBackend.Model.DataModels;
using UniversityApiBackend.DataAccess;

namespace UniversityApiBackend.Model
{
    public static class Services
    {
        public static IQueryable<User>? UserByMail(UniversityDbContext context, string mail)
        {
            if (context != null)
                return from user in context.Users
                       where user.Email.Equals(mail)
                       select user;

            return null;
        }

        public static IQueryable<Student>? StudentIfAdults(UniversityDbContext context)
        {
            if (context != null)
                return context.Students
                    .Select(student=> student)
                    .Where(student => (DateTime.Now - student.DOB).TotalDays / 365.2425 >= 18);

            return null;
        }

        public static IQueryable<Student>? StudentIfInACourse(UniversityDbContext context)
        {
            if (context != null)
                return context.Students
                    .Select(student => student)
                    .Where(student => student.Courses.Count > 0);

            return null;
        }

        public static IQueryable<Course>? CourseByLevelIfHasStudent(UniversityDbContext context, CourseLevel level)
        {
            if (context != null)
                return context.Courses
                    .Select(course => course)
                    .Where(course => course.Students.Count > 0 && course.Level == level);

            return null;
        }

        public static IQueryable<Course>? CourseByLevelCategory(UniversityDbContext context, CourseLevel level, Category category)
        {
            if (context != null)
                return context.Courses
                    .Select(course => course)
                    .Where(course => course.Level == level && course.Categories.Contains(category));

            return null;
        }

        public static IQueryable<Course>? CourseIfNoStudents(UniversityDbContext context)
        {
            if (context != null)
                return context.Courses
                    .Select(course => course)
                    .Where(course => course.Students.Count == 0);

            return null;
        }
    }
}
