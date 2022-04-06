using Microsoft.EntityFrameworkCore;
using TaskDb.Models;

namespace TaskDb
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options){}
        public DbSet<TaskTable> TaskDbSet { get; set; }
        public DbSet<TaskStatus> TaskStatusDbset { get; set; }
        public DbSet<TaskDocument> TaskDocumentDbset { get; set; }

    }
}
