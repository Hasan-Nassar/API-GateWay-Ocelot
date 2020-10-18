using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Course.Persistence
{
   public class CourseDbContext : DbContext
    {
        public DbSet<Core.Entity.Course> Courses { get; set; }
        
        public CourseDbContext(DbContextOptions<CourseDbContext> Options)
            : base(Options)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}