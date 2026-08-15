using Microsoft.EntityFrameworkCore;
using TeamFlow.API.Data;
using TeamFlow.API.DTOs;
using TeamFlow.API.Models;

namespace TeamFlow.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;
        private static readonly string[] ValidPriorities = { "Low", "Medium", "High" };
        private static readonly string[] ValidStatuses = { "To Do", "In Progress", "Done" };

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskResponseDto>> GetTasksAsync(string? status, string? assignee, string? search, string? sort)
        {
            var query = _context.Tasks.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                var normStatus = status.Trim();
                query = query.Where(t => t.Status.ToLower() == normStatus.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(assignee))
            {
                var normAssignee = assignee.Trim();
                query = query.Where(t => t.AssigneeName.ToLower() == normAssignee.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var normSearch = search.Trim().ToLower();
                query = query.Where(t => t.Title.ToLower().Contains(normSearch));
            }

            // Default sort: DueDate ascending
            if (string.Equals(sort, "dueDate", StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(sort))
            {
                query = query.OrderBy(t => t.DueDate);
            }
            else if (string.Equals(sort, "dueDateDesc", StringComparison.OrdinalIgnoreCase))
            {
                query = query.OrderByDescending(t => t.DueDate);
            }

            var tasks = await query.ToListAsync();

            return tasks.Select(MapToDto);
        }

        public async Task<TaskResponseDto?> GetTaskByIdAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task != null ? MapToDto(task) : null;
        }

        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto)
        {
            ValidateTaskInput(dto.Title, dto.DueDate, dto.Priority, dto.Status);

            var task = new TaskItem
            {
                Title = dto.Title.Trim(),
                AssigneeName = dto.AssigneeName.Trim(),
                Priority = NormalizePriority(dto.Priority),
                DueDate = DateTime.SpecifyKind(dto.DueDate, DateTimeKind.Utc),
                Status = NormalizeStatus(dto.Status),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return MapToDto(task);
        }

        public async Task<TaskResponseDto?> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return null;

            ValidateTaskInput(dto.Title, dto.DueDate, dto.Priority, dto.Status);

            task.Title = dto.Title.Trim();
            task.AssigneeName = dto.AssigneeName.Trim();
            task.Priority = NormalizePriority(dto.Priority);
            task.DueDate = DateTime.SpecifyKind(dto.DueDate, DateTimeKind.Utc);
            task.Status = NormalizeStatus(dto.Status);
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToDto(task);
        }

        public async Task<TaskResponseDto?> UpdateTaskStatusAsync(int id, string status)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return null;

            var normStatus = NormalizeStatus(status);
            if (!ValidStatuses.Contains(normStatus, StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException($"Invalid status. Must be one of: {string.Join(", ", ValidStatuses)}");
            }

            task.Status = normStatus;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToDto(task);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        private static void ValidateTaskInput(string title, DateTime dueDate, string priority, string status)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Task title is required and cannot be empty.");
            }

            // Ensure due date is not in the past (allowing today's date)
            if (dueDate.Date < DateTime.UtcNow.Date)
            {
                throw new ArgumentException("Due date cannot be in the past.");
            }

            var normPriority = NormalizePriority(priority);
            if (!ValidPriorities.Contains(normPriority, StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException($"Invalid priority '{priority}'. Must be one of: {string.Join(", ", ValidPriorities)}.");
            }

            var normStatus = NormalizeStatus(status);
            if (!ValidStatuses.Contains(normStatus, StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException($"Invalid status '{status}'. Must be one of: {string.Join(", ", ValidStatuses)}.");
            }
        }

        private static string NormalizePriority(string priority)
        {
            if (string.IsNullOrWhiteSpace(priority)) return "Medium";
            return ValidPriorities.FirstOrDefault(p => p.Equals(priority.Trim(), StringComparison.OrdinalIgnoreCase)) ?? priority.Trim();
        }

        private static string NormalizeStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return "To Do";
            return ValidStatuses.FirstOrDefault(s => s.Equals(status.Trim(), StringComparison.OrdinalIgnoreCase)) ?? status.Trim();
        }

        private static TaskResponseDto MapToDto(TaskItem task)
        {
            return new TaskResponseDto
            {
                TaskId = task.TaskId,
                Title = task.Title,
                AssigneeName = task.AssigneeName,
                Priority = task.Priority,
                DueDate = task.DueDate,
                Status = task.Status,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }
    }
}
