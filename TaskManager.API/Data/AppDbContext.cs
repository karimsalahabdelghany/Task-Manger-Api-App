using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TaskManager.API.Models;

namespace TaskManager.API.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {

        }
        public DbSet<Taskitem> Tasks { get; set; }
        public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

                // Use your actual connection string
                optionsBuilder.UseSqlServer("Server=.;Database=TaskManagerDb;Trusted_Connection=True;TrustServerCertificate=True;");

                return new AppDbContext(optionsBuilder.Options);
            }
        }
    }

}
