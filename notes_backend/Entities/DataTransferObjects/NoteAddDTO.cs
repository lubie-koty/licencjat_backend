using System.ComponentModel.DataAnnotations;

namespace notes_backend.Entities.DataTransferObjects
{
    public class NoteAddDTO
    {
        [Required(ErrorMessage = "No user id provided.")]
        public int? userId { get; set; }
        [Required(ErrorMessage = "No title provided.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "No content provided.")]
        public string? Content { get; set; }
    }
}
