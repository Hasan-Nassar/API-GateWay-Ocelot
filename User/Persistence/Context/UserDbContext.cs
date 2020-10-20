using System;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using User.Core.Entities;

namespace User.Persistence.Context
{
    public class UserDbContext: IdentityDbContext<Core.Entities.ApplicationUser>
    {
        public DbSet<Core.Entities.ApplicationUser> ApplicationUsers { get; set; }
        
        public UserDbContext(DbContextOptions<UserDbContext> Options)
            : base(Options)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
        
        public class Fix : IDesignTimeDbContextFactory<UserDbContext>
        {
            public UserDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
                optionsBuilder.UseSqlServer("Server=localhost;Database=Task3;Trusted_Connection=True;");
                return new UserDbContext(optionsBuilder.Options);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            var hash = new PasswordHasher<ApplicationUser>();
            var adminId = Guid.NewGuid().ToString();
            var roleId = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(new IdentityRole
                { Id = roleId, Name = "SuperAdmin", NormalizedName = "SuperAdmin".ToUpper() });

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                Email = "developer@gmail.com",
                NormalizedEmail = "developer@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+962788000000000",
                PhoneNumberConfirmed = true,
                PasswordHash = hash.HashPassword(null, "P@ssw0rd"),
                SecurityStamp = String.Empty,
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
        
    }
}