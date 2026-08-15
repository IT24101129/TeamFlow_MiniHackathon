using System.ComponentModel.DataAnnotations;

namespace TeamFlow.API.DTOs
{
    public class CreateTaskDto
    {
        [Required(ErrorMessage = "Task title is required.")]
        [MinLength(1, ErrorMessage = "Task title cannot be empty.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Assignee name is required.")]
        public string AssigneeName { get; set; } = string.Empty;

        [Required]
        public string Priority { get; set; } = "Medium";

        [Required(ErrorMessage = "Due date is required.")]
        public DateTime DueDate { get; set; }

        public string Status { get; set; } = "To Do";
    }

    public class UpdateTaskDto
    {
        [Required(ErrorMessage = "Task title is required.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Assignee name is required.")]
        public string AssigneeName { get; set; } = string.Empty;

        [Required]
        public string Priority { get; set; } = "Medium";

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string Status { get; set; } = "To Do";
    }

    public class UpdateTaskStatusDto
    {
        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; } = string.Empty;
    }

    public class TaskResponseDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string AssigneeName { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
