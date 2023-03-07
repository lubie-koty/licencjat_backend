using System.ComponentModel.DataAnnotations;

namespace notes_backend.Entities.DataTransferObjects
{
    public class UserRegisterDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required(ErrorMessage = "No email provided.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "No password provided.")]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? PasswordConfirmation { get; set; }
    }
}
