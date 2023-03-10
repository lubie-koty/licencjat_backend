using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using notes_backend.Services;
using notes_backend.Entities.Models;
using notes_backend.Entities.DataTransferObjects;

namespace notes_backend.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NoteService _noteService;
        public NotesController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Note>>> GetNotes(int userId)
        {
            return Ok(await _noteService.GetUserNotes(userId));
        }

        [HttpPost]
        public async Task<ActionResult<ActionResponseDTO>> PostNote([FromBody] NoteAddDTO noteData)
        {
            if (noteData == null || !ModelState.IsValid)
            {
                return BadRequest(new ActionResponseDTO
                {
                    IsSuccessful = false,
                    Errors = new List<string>() { "Invalid note data." }
                });
            }
            var result = await _noteService.AddNote(noteData);
            if (result == 0)
            {
                return BadRequest(new ActionResponseDTO
                {
                    IsSuccessful = false,
                    Errors = new List<string>() { "Error while adding a note." }
                });
            }
            return Ok(new ActionResponseDTO
            {
                IsSuccessful = true
            });
        }

        [HttpPatch("{noteId}")]
        public async Task<ActionResult<ActionResponseDTO>> PatchNote(int noteId, [FromBody] NoteEditDTO noteData)
        {
            if (noteData == null || !ModelState.IsValid)
            {
                return BadRequest(new ActionResponseDTO
                {
                    IsSuccessful = false,
                    Errors = new List<string>() { "Invalid note data." }
                });
            }
            var result = await _noteService.EditNote(noteId, noteData);
            if (result == 0)
            {
                return BadRequest(new ActionResponseDTO
                {
                    IsSuccessful = false,
                    Errors = new List<string>() { "Error while editing a note." }
                });
            }
            return Ok(new ActionResponseDTO
            {
                IsSuccessful = true
            });
        }

        [HttpDelete("{noteId}")]
        public async Task<ActionResult<ActionResponseDTO>> DeleteNote(int noteId)
        {
            var result = await _noteService.DeleteNote(noteId);
            if (result == 0)
            {
                return BadRequest(new ActionResponseDTO
                {
                    IsSuccessful = false,
                    Errors = new List<string>() { "Error while deleting a note." }
                });
            }
            return Ok(new ActionResponseDTO
            {
                IsSuccessful = true
            });
        }
    }
}
