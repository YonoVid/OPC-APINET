using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Model.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        //Add DbSets (Tables of our database)
        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Category>? Categories{ get; set; }
        public DbSet<Chapters>? Chapters { get; set; }
        public DbSet<Student>? Students{ get; set; }

        protected override void  OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var logger = _loggerFactory.CreateLogger<UniversityDbContext>();
            //optionsBuilder.LogTo(d => logger.Log(LogLevel.Information, d, new[] { DbLoggerCategory.Database.Name}));
            //optionsBuilder.EnableSensitiveDataLogging();

            //Configuration of database information level action to be saved in log
            optionsBuilder.LogTo(d => logger.Log(LogLevel.Information, d, new[] { DbLoggerCategory.Database.Name}), LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
        }

    }
}
