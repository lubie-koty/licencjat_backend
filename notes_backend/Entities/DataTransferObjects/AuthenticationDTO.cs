namespace notes_backend.Entities.DataTransferObjects
{
    public class AuthenticationDTO
    {
        public bool IsSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
    }
}
