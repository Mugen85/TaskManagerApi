using Microsoft.EntityFrameworkCore;

namespace TaskManager.Api.Data
{
    public class TaskDbContext(DbContextOptions<TaskDbContext> options) : DbContext(options)
    {
        public DbSet<TaskManager.Api.Entities.Task> Tasks { get; set; }  // rappresenta la tabella "Tasks"
    }
}
