namespace TaskManager.Api.Entities
{
    public class ToDoTask
    {
        public int Id { get; set; }                 // chiave primaria
        public string? Title { get; set; }           // titolo del task
        public string? Description { get; set; }     // descrizione del task
        public bool IsCompleted { get; set; }       // stato completato o no
    }
}
