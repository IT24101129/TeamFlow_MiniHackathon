using Microsoft.EntityFrameworkCore;
using TeamFlow.API.Models;

namespace TeamFlow.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(e => e.TaskId);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.AssigneeName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Priority).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.AssigneeName);
            });

            // Seed initial 5 tasks required by the Mini Hackathon statement
            var baseDate = new DateTime(2026, 8, 15, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem
                {
                    TaskId = 1,
                    Title = "Design homepage wireframes & layout",
                    AssigneeName = "Sarah Connor",
                    Priority = "High",
                    DueDate = baseDate.AddDays(2),
                    Status = "In Progress",
                    CreatedAt = baseDate.AddDays(-2),
                    UpdatedAt = baseDate.AddDays(-1)
                },
                new TaskItem
                {
                    TaskId = 2,
                    Title = "Implement task controller REST API",
                    AssigneeName = "John Doe",
                    Priority = "High",
                    DueDate = baseDate.AddDays(4),
                    Status = "To Do",
                    CreatedAt = baseDate.AddDays(-2),
                    UpdatedAt = baseDate.AddDays(-2)
                },
                new TaskItem
                {
                    TaskId = 3,
                    Title = "Create PostgreSQL database schema & migrations",
                    AssigneeName = "Alex Rivera",
                    Priority = "Medium",
                    DueDate = baseDate.AddDays(-1),
                    Status = "Done",
                    CreatedAt = baseDate.AddDays(-3),
                    UpdatedAt = baseDate.AddDays(-1)
                },
                new TaskItem
                {
                    TaskId = 4,
                    Title = "Test Swagger API endpoints & frontend integration",
                    AssigneeName = "Emily Chen",
                    Priority = "Medium",
                    DueDate = baseDate.AddDays(5),
                    Status = "To Do",
                    CreatedAt = baseDate.AddDays(-1),
                    UpdatedAt = baseDate.AddDays(-1)
                },
                new TaskItem
                {
                    TaskId = 5,
                    Title = "Prepare SE3090 project documentation & presentation",
                    AssigneeName = "Michael Scott",
                    Priority = "Low",
                    DueDate = baseDate.AddDays(7),
                    Status = "To Do",
                    CreatedAt = baseDate,
                    UpdatedAt = baseDate
                }
            );
        }
    }
}
