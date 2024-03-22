using Note_Microservices.Model;
using Note_Microservices.NoteEntity;

namespace Note_Microservices.IService
{
    public interface INoteService
    {
        public NoteEntity1 AddNote(NoteModel noteModel, int createdBY);
    }
}
