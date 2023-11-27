using Microsoft.EntityFrameworkCore;
using ToDoListTestTask.Models;

namespace ToDoListTestTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public DbSet<TaskModel> Tasks { get; set; }
    }
}
