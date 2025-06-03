# Documentazione: Implementazione DTO, Validazione e Test Aggiornati

Data: 03 Giugno 2025

Questo documento descrive le modifiche apportate per migliorare la gestione dei dati in ingresso/uscita dell'API, l'aggiunta di validazioni e l'aggiornamento dei test unitari per `TaskManagerAPI`.

## 1. Introduzione

Per migliorare la robustezza, la sicurezza e la manutenibilità dell'API, sono stati introdotti specifici Data Transfer Objects (DTO) per le operazioni CRUD sui task. Questi DTO includono regole di validazione per garantire l'integrità dei dati. Di conseguenza, il `TasksController` e i relativi test unitari (`TasksControllerTests`) sono stati aggiornati.

## 2. Creazione dei Data Transfer Objects (DTO)

Sono stati creati i seguenti DTO nella cartella `src/TaskManager.Api/Dtos/`:

### a. `TaskResponseDto.cs`
Utilizzato per restituire i dati di un task al client.
```csharp
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
```

### b. CreateTaskRequestDto.cs
Utilizzato per ricevere i dati dal client durante la creazione di un nuovo task. Include validazioni.
```csharp
// src/TaskManager.Api/Dtos/CreateTaskRequestDto.cs
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Dtos
{
    public class CreateTaskRequestDto
    {
        [Required(ErrorMessage = "Il titolo è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il titolo non può superare i 100 caratteri.")]
        public string? Title { get; set; }

        [StringLength(500, ErrorMessage = "La descrizione non può superare i 500 caratteri.")]
        public string? Description { get; set; }
    }
}
```

### c. UpdateTaskRequestDto.cs
Utilizzato per ricevere i dati dal client durante l'aggiornamento di un task esistente. Include validazioni.

```csharp
// src/TaskManager.Api/Dtos/UpdateTaskRequestDto.cs
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Dtos
{
    public class UpdateTaskRequestDto
    {
        [Required(ErrorMessage = "Il titolo è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il titolo non può superare i 100 caratteri.")]
        public string? Title { get; set; }

        [StringLength(500, ErrorMessage = "La descrizione non può superare i 500 caratteri.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Lo stato di completamento è obbligatorio.")]
        public bool? IsCompleted { get; set; } // bool? per gestire il caso in cui il valore non sia fornito
    }
}
```

## 3. Aggiornamento del Profilo AutoMapper (`UserProfile.cs`)

Il profilo AutoMapper è stato aggiornato per includere i mapping tra l'entità `ToDoTask` e i nuovi DTO:
```csharp
// src/TaskManager.Api/Mappings/UserProfile.cs
// ...
CreateMap<ToDoTask, TaskResponseDto>();
CreateMap<CreateTaskRequestDto, ToDoTask>();
CreateMap<UpdateTaskRequestDto, ToDoTask>();
// ...
```
## 4. Aggiornamento del Controller (`TasksController.cs`)

Il `TasksController` è stato modificato per:
* Accettare i DTO `CreateTaskRequestDto` e `UpdateTaskRequestDto` come parametri nei metodi di azione POST e PUT.
* Restituire `TaskResponseDto` nei metodi GET e come risultato della creazione.
* Affidarsi alla validazione automatica fornita dall'attributo `[ApiController]` per i DTO in input.
* Includere controlli espliciti `if (!ModelState.IsValid) { return BadRequest(ModelState); }` all'inizio dei metodi `Create` e `Update` per una gestione chiara degli errori di validazione, utile anche per facilitare i test unitari.
 
## 5. Aggiornamento e Aggiunta di Test Unitari (`TasksControllerTests.cs`)

La suite di test per `TasksController` è stata aggiornata come segue:
* I test esistenti (es. `GetAll...`, `GetById...`, `CreateAddsTask...`, `UpdateChangesValues...`) sono stati modificati per utilizzare i nuovi DTO (`TaskResponseDto`, `CreateTaskRequestDto`, `UpdateTaskRequestDto`) sia nell'input che nelle asserzioni sui risultati.
* Sono stati aggiunti nuovi test specifici per verificare la gestione degli errori di validazione:
    * `CreateReturnsBadRequestWhenTitleIsEmpty()`: Verifica che venga restituito un `400 Bad Request` se si tenta di creare un task con un titolo non valido (es. vuoto), simulando un `ModelState` non valido.
    * `UpdateReturnsBadRequestWhenTitleIsEmpty()`: Verifica un comportamento analogo per l'operazione di aggiornamento.
* Durante il processo, è stata evidenziata l'importanza di simulare correttamente `ModelState` (usando `_controller.ModelState.AddModelError(...)`) per testare unitariamente la logica di validazione all'interno delle azioni del controller quando queste si affidano a `ModelState.IsValid`.

---

Questa documentazione riflette lo stato dell'arte delle funzionalità di gestione dei task al termine della sessione di sviluppo odierna.