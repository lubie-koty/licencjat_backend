namespace notes_backend.Entities.DataTransferObjects
{
    public class ActionResponseDTO
    {
        public bool IsSuccessful { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
