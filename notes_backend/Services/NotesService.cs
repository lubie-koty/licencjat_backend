using AutoMapper;
using Microsoft.EntityFrameworkCore;
using notes_backend.Data;
using notes_backend.Entities.DataTransferObjects;
using notes_backend.Entities.Models;
using notes_backend.Interfaces; 

namespace notes_backend.Services
{
    public class NotesService : INotesService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public NotesService(DataContext dataContext, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<Note>> GetUserNotes(string userId)
        {
            return await _dataContext.Notes
                .Where(n => n.UserId == userId)
                .ToListAsync();
        }

        public async Task<ActionResponseDTO> AddNote(string userId, NoteDTO noteData)
        {
            Note note = _mapper.Map<Note>(noteData);
            note.UserId = userId;
            note.Created = DateTime.Now.ToUniversalTime();

            _dataContext.Notes.Add(note);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return new ActionResponseDTO
                {
                    IsSuccessful = false,
                    Errors = ex.Entries.Select(e => e.ToString())
                };
            }

            return new ActionResponseDTO { IsSuccessful = true };
        }

        public async Task<ActionResponseDTO> EditNote(int noteId, NoteDTO noteData)
        {
            var note = await _dataContext.Notes.FindAsync(noteId);
            if (note == null)
            {
                return new ActionResponseDTO
                {
                    IsSuccessful = false,
                    Errors = new List<string>() { "Note not found." }
                };
            }

            note.Title = noteData.Title;
            note.Content = noteData.Content;
            note.Updated = DateTime.Now.ToUniversalTime();
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return new ActionResponseDTO
                {
                    IsSuccessful = false,
                    Errors = ex.Entries.Select(e => e.ToString())
                };
            }

            return new ActionResponseDTO { IsSuccessful = true };
        }

        public async Task<ActionResponseDTO> DeleteNote(int noteId)
        {
            var note = await _dataContext.Notes.FindAsync(noteId);
            if (note == null)
            {
                return new ActionResponseDTO
                {
                    IsSuccessful = false,
                    Errors = new List<string>() { "Note not found." }
                };
            }

            _dataContext.Remove(note);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return new ActionResponseDTO
                {
                    IsSuccessful = false,
                    Errors = ex.Entries.Select(e => e.ToString())
                };
            }

            return new ActionResponseDTO { IsSuccessful = true };
        }
    }
}
