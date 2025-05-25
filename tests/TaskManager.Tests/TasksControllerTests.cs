using Xunit;
using TaskManager.Api.Controllers;
using TaskManager.Api.Entities;
using TaskManager.Api.Data;
using TaskManager.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TaskManager.Api.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Tests
{
    public class TasksControllerTests
    {
        private readonly TasksController _controller;
        private readonly TaskDbContext _context;
        private readonly IMapper _mapper;

        public TasksControllerTests()
        {
            // Arrange: configura il DbContext InMemory e AutoMapper
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TaskDbContext(options);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>());
            _mapper = config.CreateMapper();

            _controller = new TasksController(_context, _mapper);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllReturnsEmptyListWhenNoTasksExist()
        {
            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var tasks = Assert.IsType<List<TaskAllDto>>(okResult.Value);
            Assert.Empty(tasks);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateAddsTaskAndReturnsCreatedDto()
        {
            // Arrange
            var dto = new TaskAllDto { Title = "Nuovo task", IsCompleted = false };

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedDto = Assert.IsType<TaskAllDto>(createdResult.Value);
            Assert.Equal("Nuovo task", returnedDto.Title);
            Assert.False(returnedDto.IsCompleted);
            Assert.NotEqual(0, returnedDto.Id); // ID assegnato automaticamente
        }

        [Fact]
        public async System.Threading.Tasks.Task GetByIdReturnsTaskWhenExists()
        {
            // Arrange
            var task = new ToDoTask { Title = "Test", IsCompleted = false };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetById(task.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<TaskAllDto>(okResult.Value);
            Assert.Equal("Test", dto.Title);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateChangesValuesCorrectly()
        {
            // Arrange
            var task = new ToDoTask { Title = "Vecchio titolo", IsCompleted = false };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var updatedDto = new TaskAllDto
            {
                Id = task.Id,
                Title = "Titolo aggiornato",
                IsCompleted = true
            };

            // Act
            var result = await _controller.Update(task.Id, updatedDto);

            // Assert
            Assert.IsType<NoContentResult>(result);

            var updatedTask = await _context.Tasks.FindAsync(task.Id);
            Assert.Equal("Titolo aggiornato", updatedTask!.Title);
            Assert.True(updatedTask.IsCompleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteRemovesTaskFromDb()
        {
            // Arrange
            var task = new ToDoTask { Title = "Da eliminare", IsCompleted = false };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Delete(task.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var deletedTask = await _context.Tasks.FindAsync(task.Id);
            Assert.Null(deletedTask);
        }
    }
}
