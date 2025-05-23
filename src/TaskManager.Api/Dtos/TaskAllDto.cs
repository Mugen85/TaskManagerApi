namespace TaskManager.Api.Dtos
{
    public class TaskAllDto
    {
        public int Id { get; set; }                 // viene mostrato
        public string? Title { get; set; }           // viene mostrato
        public bool IsCompleted { get; set; }       // viene mostrato
    }
}
