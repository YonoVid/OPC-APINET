using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Model.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        {

        }

        //Add DbSets (Tables of our database)
        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Category>? Categories{ get; set; }
        public DbSet<Chapters>? Chapters { get; set; }
        public DbSet<Student>? Students{ get; set; }
    }
}
