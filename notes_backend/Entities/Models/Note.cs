namespace notes_backend.Entities.Models
{
    public class Note
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
