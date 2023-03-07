using Microsoft.AspNetCore.Identity;

namespace notes_backend.Entities.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
