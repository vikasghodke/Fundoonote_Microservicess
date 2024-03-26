using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Note_Microservices.Context;
using Note_Microservices.IService;
using Note_Microservices.Model;
using Note_Microservices.NoteEntity;
using System.Net.Http.Headers;

namespace Note_Microservices.Service
{
    public class NoteService : INoteService
    {
        private readonly NoteContext _noteContext;
        public NoteService(NoteContext noteContext)
        {
            this._noteContext = noteContext;
        }

        public async Task<NoteEntity1> AddNote(NoteModel noteModel, int createdBY, string token)
        {
            NoteEntity1 entity = new NoteEntity1();
            entity.NoteTitle = noteModel.NoteTitle;
            entity.NoteDescription = noteModel.NoteDescription;
            entity.Color = noteModel.Color;
            entity.CreatedBy = createdBY;

            entity.User = await GetUserDetails(token);
           
            _noteContext.NoteTable.Add(entity);
            _noteContext.SaveChanges();

            return entity;

        }
        public List<NoteEntity1> GetNotes(int userId)
        {
            var notes = _noteContext.NoteTable.Where(e => e.CreatedBy == userId).ToList();
            return notes;
        }
        public bool EditNote(int noteId, int userId, NoteModel model)
        {
            var note = _noteContext.NoteTable.FirstOrDefault(e => e.NoteId == noteId && e.CreatedBy == userId);
            if (note != null)
            {
                note.NoteTitle = model.NoteTitle;
                note.NoteDescription = model.NoteDescription;
                note.Color = model.Color;

                _noteContext.SaveChanges();

                return true;
            }
            return false;
        }
        public bool DeleteNote(int noteId, int userId)
        {
            var note = _noteContext.NoteTable.FirstOrDefault(e => e.NoteId == noteId && e.CreatedBy == userId);
            if (note != null)
            {
                _noteContext.NoteTable.Remove(note);
                _noteContext.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<UserEntity1> GetUserDetails(string token)
        {
            HttpClient httpClient = new HttpClient();

            
            string url = "https://localhost:7269/api/User";
            HttpRequestMessage request=new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
             
            if (responseMessage.IsSuccessStatusCode)
            {
                string responseContent = await responseMessage.Content.ReadAsStringAsync();

                ResponseModel<UserEntity1> response = JsonConvert.DeserializeObject<ResponseModel<UserEntity1>>(responseContent);

                if (response != null && response.Data != null)
                {

                    return response.Data;
                }
                else
                {
                    throw new Exception("Failed to retrive user data");
                }
            }
            else
            {
                throw new Exception("Failed to retrive user data");
            }
        }
    }
}
