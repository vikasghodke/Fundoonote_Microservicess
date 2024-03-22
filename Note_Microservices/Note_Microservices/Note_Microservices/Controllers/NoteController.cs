using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Note_Microservices.IService;
using Note_Microservices.Model;
using Note_Microservices.NoteEntity;
using Note_Microservices.Service;
using System.Security.Claims;

namespace Note_Microservices.Controllers
{
    [Route("api/Notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        public readonly INoteService _noteService;
        public NoteController(INoteService noteService)
        {
            this._noteService = noteService;
        }
        [HttpPost("Add")]
        public ResponseModel<NoteModel> AddNote(NoteModel noteModel)
        {
            ResponseModel<NoteModel> response = new ResponseModel<NoteModel>();
            try
            {
                var _userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int createBY = Convert.ToInt32(_userID);

                var result = _noteService.AddNote(noteModel, createBY);
                if (result != null)
                {
                    response.Message = "Note created successfully.";
                    response.Data = noteModel;
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        [HttpGet("View")]
        public ResponseModel<List<NoteEntity1>> GetNotes()
        {
            var responseModel = new ResponseModel<List<NoteEntity1>>();
            try
            {
                var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int userId = Convert.ToInt32(_userId);
                var notes = _noteService.GetNotes(userId);

                if (notes.Count != 0)
                {
                    responseModel.Message = "Notes retrived successfully.";
                    responseModel.Data = notes;
                }
                else
                {
                    responseModel.Success = false;
                    responseModel.Message = "There is no note.";
                }
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
            }
            return responseModel;
        }
       
      
    }
}
       
