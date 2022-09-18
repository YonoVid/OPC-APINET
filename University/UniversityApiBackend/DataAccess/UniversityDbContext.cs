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
    }
}
