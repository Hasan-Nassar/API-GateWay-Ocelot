using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace User.Persistence.Context
{
    public class UserDbContext: DbContext
    {
        public DbSet<Core.Entities.User> Users { get; set; }
        
        public UserDbContext(DbContextOptions<UserDbContext> Options)
            : base(Options)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}