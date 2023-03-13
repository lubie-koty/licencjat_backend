using System.ComponentModel.DataAnnotations;

namespace notes_backend.Entities.DataTransferObjects
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
