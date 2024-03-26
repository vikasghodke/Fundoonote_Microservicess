using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ResponseModel<NoteEntity1>> AddNote(NoteModel noteModel)
        {
            ResponseModel<NoteEntity1> response = new ResponseModel<NoteEntity1>();
            try
            {
                
                var _userID = User.FindFirstValue("UserID");
                int createBY = Convert.ToInt32(_userID);

                var authenticateresult = await HttpContext.AuthenticateAsync();
                var token = authenticateresult.Properties.GetTokenValue("access_token");




                var result = _noteService.AddNote(noteModel, createBY, token);
                if (result != null)
                {
                    response.Message = "Note created successfully.";
                    response.Data = await result;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid";
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
        [Authorize]
        public ResponseModel<List<NoteEntity1>> GetNotes()
        {
            var responseModel = new ResponseModel<List<NoteEntity1>>();
            try
            {
                var _userId = User.FindFirstValue("UserID");
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
        [HttpPut]
        [Authorize]
        public ResponseModel<NoteModel> EditNote(int noteId, int userId, NoteModel model)
        {
            ResponseModel<NoteModel> response = new ResponseModel<NoteModel>();
            try
            {

                var _userID = User.FindFirstValue("UserID");
                int UserId = Convert.ToInt32(_userID);
                var check = _noteService.EditNote(noteId, userId, model);
                if (check)
                {
                    response.Message = "Note edited successfully.";
                    response.Data = model;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Note not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }
        [HttpDelete]
        [Authorize]
        public ResponseModel<NoteModel> DeleteNote(int noteId)
        {
            ResponseModel<NoteModel> response = new ResponseModel<NoteModel>();
            try
            {
                var _userID = User.FindFirstValue("UserID");
                int UserID = Convert.ToInt32(_userID);
                var check = _noteService.DeleteNote(noteId, UserID);

                if (check)
                {
                    response.Message = "Note edited successfully.";

                }
                else
                {
                    response.Success = false;
                    response.Message = "Something Went Wrong";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }


    }
}
       
