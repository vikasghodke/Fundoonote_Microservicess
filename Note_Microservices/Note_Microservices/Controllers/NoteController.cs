using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Note_Microservices.Model;
using Note_Microservices.Service;

namespace Note_Microservices.Controllers
{
    [Route("api/Notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        public readonly NoteService _noteService;
        public NoteController(NoteService noteService)
        {
            this._noteService = noteService;
        }
        [HttpPost("Add")]
        public IActionResult AddNote(NoteModel noteModel, int createdBY)
        {
            var resu=
        }
    }
}
