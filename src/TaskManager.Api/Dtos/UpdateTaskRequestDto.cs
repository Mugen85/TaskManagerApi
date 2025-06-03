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
        public bool? IsCompleted { get; set; } // Usiamo bool? per distinguere un valore non fornito da un false esplicito se necessario,
                                               // ma per un update, è probabile che tutti i campi vengano forniti.
                                               // Se il client DEVE inviare questo campo, allora bool è sufficiente.
                                               // Per semplicità, usiamo bool e assumiamo che il client lo invii sempre.
    }
}