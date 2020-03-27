using DLL.Model;
using Microsoft.EntityFrameworkCore;

namespace DLL.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}