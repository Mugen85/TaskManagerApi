using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entities;
using Task = TaskManager.Api.Entities.ToDoTask;

namespace TaskManager.Tests
{
    public class TaskDbContextTests
    {
        private static DbContextOptions<TaskDbContext> GetInMemoryOptions()
        {
            return new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // db isolato per ogni test
                .Options;
        }

        [Fact]
        public void Aggiunge_Task_Correttamente()
        {
            // Arrange
            var options = GetInMemoryOptions();

            // Act
            using (var context = new TaskDbContext(options))
            {
                var task = new ToDoTask
                {
                    Title = "Scrivere test",
                    Description = "Test sul DbContext",
                    IsCompleted = false
                };

                context.Tasks.Add(task);
                context.SaveChanges();
            }

            // Assert
            using (var context = new TaskDbContext(options))
            {
                Assert.Equal(1, context.Tasks.Count());
            }
        }

        [Fact]
        public void Legge_Task_Salvato()
        {
            // Arrange
            var options = GetInMemoryOptions();
            var titolo = "Leggere dati";

            using (var context = new TaskDbContext(options))
            {
                context.Tasks.Add(new ToDoTask
                {
                    Title = titolo,
                    Description = "Leggere da InMemory DB",
                    IsCompleted = false
                });

                context.SaveChanges();
            }

            // Act + Assert
            using (var context = new TaskDbContext(options))
            {
                var task = context.Tasks.First();
                Assert.Equal(titolo, task.Title);
            }
        }

        [Fact]
        public void Verifica_IsCompleted_False_Di_Default()
        {
            // Arrange
            var options = GetInMemoryOptions();

            using (var context = new TaskDbContext(options))
            {
                context.Tasks.Add(new ToDoTask
                {
                    Title = "Controllo completamento",
                    Description = "Verifica flag booleano",
                    IsCompleted = false
                });

                context.SaveChanges();
            }

            // Assert
            using (var context = new TaskDbContext(options))
            {
                var task = context.Tasks.First();
                Assert.False(task.IsCompleted);
            }
        }
    }
}
