using Microsoft.EntityFrameworkCore;
using Note_Microservices.Context;
using Note_Microservices.IService;
using Note_Microservices.Model;
using Note_Microservices.NoteEntity;

namespace Note_Microservices.Service
{
    public class NoteService : INoteService
    {
        private readonly NoteContext _noteContext;
        public NoteService(NoteContext noteContext)
        {
            this._noteContext = noteContext;
        }

        public NoteEntity1 AddNote(NoteModel noteModel, int createdBY)
        {
            NoteEntity1 entity = new NoteEntity1();
            entity.NoteTitle = noteModel.NoteTitle;
            entity.NoteDescription = noteModel.NoteDescription;
            entity.Color = noteModel.Color;
            entity.CreatedBy = createdBY;

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
            var note=_noteContext.NoteTable.FirstOrDefault(e=>e.NoteId==noteId && e.CreatedBy == userId);
            if (note != null)
            {
                _noteContext.NoteTable.Remove(note);
                _noteContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
