using Microsoft.EntityFrameworkCore;
using StudentsWebApi.Models;

namespace StudentsWebApi.DataContext
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
