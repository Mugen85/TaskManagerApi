using Xunit;
using TaskManager.Api.Controllers;
using TaskManager.Api.Entities; // Rimane per ToDoTask
using TaskManager.Api.Data;
using TaskManager.Api.Dtos; // Questo namespace dovrebbe contenere tutti i DTO
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TaskManager.Api.Mappings;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Aggiunto per List<T>
using System.Threading.Tasks; // Aggiunto per Task

namespace TaskManager.Tests
{
    public class TasksControllerTests
    {
        private readonly TasksController _controller;
        private readonly TaskDbContext _context;
        private readonly IMapper _mapper;

        public TasksControllerTests()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TaskDbContext(options);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()); // Assumendo che UserProfile contenga tutti i mapping necessari
            _mapper = config.CreateMapper();

            _controller = new TasksController(_context, _mapper);
        }

        [Fact]
        public async Task GetAllReturnsEmptyListWhenNoTasksExist()
        {
            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Modificato: Aspetta List<TaskResponseDto>
            var tasks = Assert.IsType<List<TaskResponseDto>>(okResult.Value);
            Assert.Empty(tasks);
        }

        [Fact]
        public async Task CreateAddsTaskAndReturnsCreatedDto()
        {
            // Arrange
            // Modificato: Usa CreateTaskRequestDto
            var createTaskDto = new CreateTaskRequestDto
            {
                Title = "Nuovo task",
                Description = "Descrizione del nuovo task"
            };

            // Act
            var result = await _controller.Create(createTaskDto); // Passa il nuovo DTO

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            // Modificato: Aspetta TaskResponseDto come valore restituito
            var returnedDto = Assert.IsType<TaskResponseDto>(createdResult.Value);
            Assert.Equal("Nuovo task", returnedDto.Title);
            Assert.Equal("Descrizione del nuovo task", returnedDto.Description);
            Assert.False(returnedDto.IsCompleted); // Come impostato di default nel controller
            Assert.NotEqual(0, returnedDto.Id);
        }

        [Fact]
        public async Task GetByIdReturnsTaskWhenExists()
        {
            // Arrange
            var taskEntity = new ToDoTask { Title = "Test GetById", Description = "Descrizione Test", IsCompleted = false };
            _context.Tasks.Add(taskEntity);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetById(taskEntity.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Modificato: Aspetta TaskResponseDto
            var dto = Assert.IsType<TaskResponseDto>(okResult.Value);
            Assert.Equal(taskEntity.Title, dto.Title);
            Assert.Equal(taskEntity.Description, dto.Description);
            Assert.Equal(taskEntity.IsCompleted, dto.IsCompleted);
        }

        [Fact]
        public async Task UpdateChangesValuesCorrectly()
        {
            // Arrange
            var originalTaskEntity = new ToDoTask { Title = "Vecchio titolo", Description = "Vecchia descrizione", IsCompleted = false };
            _context.Tasks.Add(originalTaskEntity);
            await _context.SaveChangesAsync();

            // Modificato: Usa UpdateTaskRequestDto
            var updateTaskDto = new UpdateTaskRequestDto
            {
                Title = "Titolo aggiornato",
                Description = "Descrizione aggiornata",
                IsCompleted = true
            };

            // Act
            // Chiama Update con il nuovo DTO
            var result = await _controller.Update(originalTaskEntity.Id, updateTaskDto);

            // Assert
            Assert.IsType<NoContentResult>(result);

            var updatedTaskEntity = await _context.Tasks.FindAsync(originalTaskEntity.Id);
            Assert.NotNull(updatedTaskEntity);
            Assert.Equal(updateTaskDto.Title, updatedTaskEntity.Title);
            Assert.Equal(updateTaskDto.Description, updatedTaskEntity.Description);
            Assert.Equal(updateTaskDto.IsCompleted, updatedTaskEntity.IsCompleted);
        }

        [Fact]
        public async Task DeleteRemovesTaskFromDb()
        {
            // Arrange
            var taskEntity = new ToDoTask { Title = "Da eliminare", Description = "Test", IsCompleted = false };
            _context.Tasks.Add(taskEntity);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Delete(taskEntity.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var deletedTask = await _context.Tasks.FindAsync(taskEntity.Id);
            Assert.Null(deletedTask);
        }

        // Potresti voler aggiungere test per i casi di fallimento della validazione
        // Ad esempio, per il metodo Create con un titolo mancante.
        [Fact]
        public async Task CreateReturnsBadRequestWhenTitleIsEmpty()
        {
            // Arrange
            var createTaskDto = new CreateTaskRequestDto { Title = string.Empty, Description = "Test" }; // Titolo non valido

            // Simula la validazione del modello (necessario se non si testa l'intero pipeline HTTP)
            _controller.ModelState.AddModelError("Title", "Il titolo è obbligatorio.");

            // Act
            var result = await _controller.Create(createTaskDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            // Verifica che la risposta contenga gli errori di validazione (opzionale, ma buon test)
            // Assert.NotNull(badRequestResult.Value); 
        }

        [Fact]
        public async Task UpdateReturnsBadRequestWhenTitleIsEmpty()
        {
            // Arrange
            // Puoi decidere se mantenere la creazione di un'entità esistente o usare un ID fittizio.
            // Se usi un ID fittizio, assicurati che il test fallisca per la ragione giusta (validazione)
            // e non perché l'entità non è trovata, se la logica del controller arrivasse a quel punto.
            // In questo caso, dato che ci aspettiamo un fallimento della validazione PRIMA del recupero dell'entità,
            // un ID fittizio andrebbe bene, ma usare un'entità esistente è più robusto se la logica cambiasse.
            // Manteniamo l'entità per coerenza con il test originale.
            var originalTaskEntity = new ToDoTask { Title = "Titolo Originale", Description = "Descrizione Originale", IsCompleted = false };
            _context.Tasks.Add(originalTaskEntity);
            await _context.SaveChangesAsync();

            var updateTaskDto = new UpdateTaskRequestDto
            {
                Title = string.Empty, // Titolo non valido
                Description = "Descrizione Aggiornata",
                IsCompleted = true
            };

            _controller.ModelState.Clear(); // Buona pratica per isolare lo stato del test
            _controller.ModelState.AddModelError("Title", "Il titolo è obbligatorio."); // Simula il fallimento della validazione

            // Act
            var result = await _controller.Update(originalTaskEntity.Id, updateTaskDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            // Opzionale: Ulteriore verifica del contenuto dell'errore, se lo desideri
            // Assert.NotNull(badRequestResult.Value);
            // if (badRequestResult.Value is ValidationProblemDetails validationDetails)
            // {
            //     Assert.True(validationDetails.Errors.ContainsKey("Title"));
            // }
        }
    }
}