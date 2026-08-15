using Microsoft.AspNetCore.Mvc;
using TeamFlow.API.DTOs;
using TeamFlow.API.Services;

namespace TeamFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Get all tasks with optional search, status filter, assignee filter, and sorting.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetTasks(
            [FromQuery] string? status,
            [FromQuery] string? assignee,
            [FromQuery] string? search,
            [FromQuery] string? sort)
        {
            var tasks = await _taskService.GetTasksAsync(status, assignee, search, sort);
            return Ok(tasks);
        }

        /// <summary>
        /// Get a single task by ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskResponseDto>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound(new { message = $"Task with ID {id} not found." });
            }
            return Ok(task);
        }

        /// <summary>
        /// Create a new task with validation.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TaskResponseDto>> CreateTask([FromBody] CreateTaskDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdTask = await _taskService.CreateTaskAsync(dto);
                return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.TaskId }, createdTask);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing task completely.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TaskResponseDto>> UpdateTask(int id, [FromBody] UpdateTaskDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedTask = await _taskService.UpdateTaskAsync(id, dto);
                if (updatedTask == null)
                {
                    return NotFound(new { message = $"Task with ID {id} not found." });
                }
                return Ok(updatedTask);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update only the status of a task.
        /// </summary>
        [HttpPatch("{id:int}/status")]
        public async Task<ActionResult<TaskResponseDto>> UpdateTaskStatus(int id, [FromBody] UpdateTaskStatusDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Status))
            {
                return BadRequest(new { message = "Status value is required." });
            }

            try
            {
                var updatedTask = await _taskService.UpdateTaskStatusAsync(id, dto.Status);
                if (updatedTask == null)
                {
                    return NotFound(new { message = $"Task with ID {id} not found." });
                }
                return Ok(updatedTask);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete a task by ID.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var success = await _taskService.DeleteTaskAsync(id);
            if (!success)
            {
                return NotFound(new { message = $"Task with ID {id} not found." });
            }
            return NoContent();
        }
    }
}
