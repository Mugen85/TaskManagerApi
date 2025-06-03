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

        // IsCompleted non è solitamente impostato alla creazione, 
        // ma potresti volerlo includere con un valore di default (es. false).
        // Per ora lo omettiamo, assumendo che un nuovo task non sia completato.
    }
}