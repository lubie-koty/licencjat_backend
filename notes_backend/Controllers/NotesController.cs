using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using notes_backend.Entities.Models;
using notes_backend.Entities.DataTransferObjects;
using notes_backend.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace notes_backend.Controllers
{
    [Route("api/notes")]
    [Authorize]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _notesService;
        private readonly UserManager<User> _userManager;

        public NotesController(INotesService notesService, UserManager<User> userManager)
        {
            _notesService = notesService;
            _userManager = userManager;
        }

        private string GetCurrentUserId() => _userManager.GetUserId(User);

        [HttpGet]
        public async Task<ActionResult<List<Note>>> GetNotes()
        {
            return Ok(await _notesService.GetUserNotes(GetCurrentUserId()));
        }

        [HttpPost]
        public async Task<ActionResult<ActionResponseDTO>> PostNote([FromBody] NoteDTO noteData)
        {
            if (noteData == null || !ModelState.IsValid)
            {
                return BadRequest(new ActionResponseDTO
                {
                    IsSuccessful = false,
                    Errors = new List<string>() { "Invalid note data." }
                });
            }

            var result = await _notesService.AddNote(GetCurrentUserId(), noteData);
            if (result.IsSuccessful == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch("{noteId}")]
        public async Task<ActionResult<ActionResponseDTO>> PatchNote(int noteId, [FromBody] NoteDTO noteData)
        {
            if (noteData == null || !ModelState.IsValid)
            {
                return BadRequest(new ActionResponseDTO
                {
                    IsSuccessful = false,
                    Errors = new List<string>() { "Invalid note data." }
                });
            }

            var result = await _notesService.EditNote(noteId, noteData);
            if (result.IsSuccessful == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{noteId}")]
        public async Task<ActionResult<ActionResponseDTO>> DeleteNote(int noteId)
        {
            var result = await _notesService.DeleteNote(noteId);
            if (result.IsSuccessful == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
