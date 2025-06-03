using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// Aggiorna i using per i nuovi DTO se necessario, o assicurati che il namespace li renda accessibili
using TaskManager.Api.Dtos; // Questo dovrebbe già coprire TaskResponseDto, CreateTaskRequestDto, UpdateTaskRequestDto se sono nello stesso namespace di TaskAllDto
using AutoMapper;
using TaskManager.Api.Data;
using TaskManager.Api.Entities; 
using Microsoft.AspNetCore.Authorization;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Potresti voler aggiungere [Authorize] a livello di controller se tutte le azioni richiedono autenticazione
    // [Authorize] 
    public class TasksController(TaskDbContext context, IMapper mapper) : ControllerBase
    {
        // GET: api/tasks
        [Authorize] // Mantenuto come da tuo codice originale
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetAll() // Modificato: TaskAllDto -> TaskResponseDto
        {
            var tasks = await context.Tasks.ToListAsync();
            var taskDtos = mapper.Map<List<TaskResponseDto>>(tasks); // Modificato: TaskAllDto -> TaskResponseDto
            return Ok(taskDtos);
        }

        // GET: api/tasks/{id}
        // Considera se anche questo endpoint necessita di [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskResponseDto>> GetById(int id) // Modificato: TaskAllDto -> TaskResponseDto
        {
            var task = await context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            var taskDto = mapper.Map<TaskResponseDto>(task); // Modificato: TaskAllDto -> TaskResponseDto
            return Ok(taskDto);
        }

        // POST: api/tasks
        // Aggiungi [Authorize] se la creazione di task richiede autenticazione
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TaskResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TaskResponseDto>> Create([FromBody] CreateTaskRequestDto createTaskDto) // Modificato: TaskAllDto -> CreateTaskRequestDto
        {
            // AGGIUNTA: Controllo esplicito di ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // La validazione (dagli attributi in CreateTaskRequestDto) è gestita automaticamente da ASP.NET Core
            // grazie a [ApiController]. Se ModelState non è valido, verrà restituito un 400 Bad Request.
            // Non è necessario scrivere: if (!ModelState.IsValid) return BadRequest(ModelState);

            var task = mapper.Map<ToDoTask>(createTaskDto); // Modificato: mappa da CreateTaskRequestDto

            // Imposta IsCompleted a false di default per i nuovi task, se non presente nel DTO
            task.IsCompleted = false;

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var taskResponseDto = mapper.Map<TaskResponseDto>(task); // Modificato: mappa a TaskResponseDto per la risposta

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, taskResponseDto);
        }

        // PUT: api/tasks/{id}
        // Aggiungi [Authorize] se l'aggiornamento di task richiede autenticazione
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskRequestDto updateTaskDto) // Modificato: TaskAllDto -> UpdateTaskRequestDto
        {
            // AGGIUNTA: Controllo esplicito di ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = await context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            // Applica i valori dal DTO all'entità esistente usando AutoMapper
            // Questo sovrascriverà solo le proprietà definite in UpdateTaskRequestDto e mappate in UserProfile
            mapper.Map(updateTaskDto, task);

            // Nota: Se vuoi un controllo più granulare su cosa viene aggiornato, 
            // potresti farlo manualmente come prima, ma AutoMapper è più conciso per l'aggiornamento completo.
            // Esempio di aggiornamento manuale se preferito (ma AutoMapper è meglio):
            // task.Title = updateTaskDto.Title;
            // task.Description = updateTaskDto.Description; // Assicurati che Description sia in UpdateTaskRequestDto se vuoi aggiornarlo
            // task.IsCompleted = updateTaskDto.IsCompleted ?? task.IsCompleted; // Gestisci nullo se IsCompleted è nullable nel DTO

            await context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/tasks/{id}
        // Aggiungi [Authorize] se l'eliminazione di task richiede autenticazione
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            context.Tasks.Remove(task);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}