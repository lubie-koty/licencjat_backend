using notes_backend.Entities.DataTransferObjects;
using notes_backend.Entities.Models;

namespace notes_backend.Interfaces
{
    public interface INotesService
    {
        public Task<List<Note>> GetUserNotes(string userId);
        public Task<ActionResponseDTO> AddNote(string userId, NoteDTO noteData);
        public Task<ActionResponseDTO> EditNote(int noteId, NoteDTO noteData);
        public Task<ActionResponseDTO> DeleteNote(int noteId);
    }
}
