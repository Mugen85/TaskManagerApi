using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Dtos;            // per TaskAllDto
using AutoMapper;
using TaskManager.Api.Data;
using ToDoTask = TaskManager.Api.Entities.ToDoTask; // Questo ti dà accesso alla classe Task


namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]        // la route sarà: /api/tasks
    public class TasksController(TaskDbContext context, IMapper mapper) : ControllerBase
    {

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskAllDto>>> GetAll()
        {
            var tasks = await context.Tasks.ToListAsync();               // carica tutte le entità dal DB
            var taskDtos = mapper.Map<List<TaskAllDto>>(tasks);         // le converte in DTO
            return Ok(taskDtos);                                         // restituisce 200 OK con la lista
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskAllDto>> GetById(int id)
        {
            var task = await context.Tasks.FindAsync(id);               // cerca l'entità per ID
            if (task == null) return NotFound();                         // se non trovata → 404

            var taskDto = mapper.Map<TaskAllDto>(task);                // la converte in DTO
            return Ok(taskDto);                                         // restituisce 200 OK
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskAllDto>> Create(TaskAllDto dto)
        {
            var task = mapper.Map<ToDoTask>(dto);                     // converte da DTO a modello
            context.Tasks.Add(task);                                   // aggiunge l'entità al contesto
            await context.SaveChangesAsync();                          // salva nel DB

            var taskDto = mapper.Map<TaskAllDto>(task);                // riconverte il risultato (con ID aggiornato)

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, taskDto); // 201 Created con location
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskAllDto dto)
        {
            var task = await context.Tasks.FindAsync(id);              // cerca l'entità nel DB
            if (task == null) return NotFound();                        // se non trovata → 404

            // aggiorna le proprietà con i valori del DTO
            task.Title = dto.Title;
            task.IsCompleted = dto.IsCompleted;

            await context.SaveChangesAsync();                          // salva nel DB
            return NoContent();                                         // 204 No Content
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await context.Tasks.FindAsync(id);              // cerca l'entità
            if (task == null) return NotFound();                        // 404 se non trovata

            context.Tasks.Remove(task);                                // rimuove l'entità
            await context.SaveChangesAsync();                          // salva nel DB

            return NoContent();                                         // 204 No Content
        }
    }
}
