// src/TaskManager.Api/Dtos/TaskResponseDto.cs
namespace TaskManager.Api.Dtos
{
    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}