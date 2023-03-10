using AutoMapper;
using Microsoft.EntityFrameworkCore;
using notes_backend.Data;
using notes_backend.Entities.DataTransferObjects;
using notes_backend.Entities.Models;

namespace notes_backend.Services
{
    public class NoteService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public NoteService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<List<Note>> GetUserNotes(int userId)
        {
            return await _dataContext.Notes
                .Where(n => n.userId == userId)
                .ToListAsync();
        }

        public async Task<int> AddNote(NoteAddDTO noteData)
        {
            Note note = _mapper.Map<Note>(noteData);
            note.Created = DateTime.Now;
            _dataContext.Notes.Add(note);
            return await _dataContext.SaveChangesAsync();
        }

        public async Task<int> EditNote(int noteId, NoteEditDTO noteData)
        {
            var note = await _dataContext.Notes.FindAsync(noteId);
            if (note == null)
            {
                return 0;
            }
            note.Title = noteData.Title;
            note.Content = noteData.Content;
            note.Updated = DateTime.Now;

            return await _dataContext.SaveChangesAsync();
        }

        public async Task<int> DeleteNote(int noteId)
        {
            var note = await _dataContext.Notes.FindAsync(noteId);
            if (note == null)
            {
                return 0;
            }
            _dataContext.Remove(note);
            return await _dataContext.SaveChangesAsync();
        }
    }
}
