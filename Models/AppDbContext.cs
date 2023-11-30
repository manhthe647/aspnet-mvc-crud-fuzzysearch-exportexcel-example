using Microsoft.EntityFrameworkCore;

namespace QuanLySinhVien.WebAppMvc.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Professor> Professors{ get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; } 
    }
}
