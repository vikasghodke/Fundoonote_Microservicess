using Note_Microservices.Context;
using Note_Microservices.IService;
using Note_Microservices.Model;
using Note_Microservices.NoteEntity;

namespace Note_Microservices.Service
{
    public class NoteService: INoteService
    {
        private readonly NoteContext _noteContext;
        public NoteService(NoteContext noteContext)
        {
            this._noteContext = noteContext;
        }

        public NoteEntity1 AddNote(NoteModel noteModel,int createdBY)
        {
            NoteEntity1 entity= new NoteEntity1();
            entity.NoteTitle = noteModel.NoteTitle;
            entity.Color= noteModel.Color;
            entity.CreatedBy= createdBY;

            _noteContext.NoteTable.Add(entity);
            _noteContext.SaveChanges();

            return entity;

        }
    }
}
