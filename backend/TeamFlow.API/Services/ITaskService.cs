using TeamFlow.API.DTOs;

namespace TeamFlow.API.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponseDto>> GetTasksAsync(string? status, string? assignee, string? search, string? sort);
        Task<TaskResponseDto?> GetTaskByIdAsync(int id);
        Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto);
        Task<TaskResponseDto?> UpdateTaskAsync(int id, UpdateTaskDto dto);
        Task<TaskResponseDto?> UpdateTaskStatusAsync(int id, string status);
        Task<bool> DeleteTaskAsync(int id);
    }
}
