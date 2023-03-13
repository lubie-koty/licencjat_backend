using System.ComponentModel.DataAnnotations;

namespace notes_backend.Entities.DataTransferObjects
{
    public class NoteDTO
    {
        [Required(ErrorMessage = "No title provided.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "No content provided.")]
        public string? Content { get; set; }
    }
}
