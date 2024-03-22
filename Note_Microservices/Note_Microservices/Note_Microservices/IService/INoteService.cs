using Note_Microservices.Model;
using Note_Microservices.NoteEntity;

namespace Note_Microservices.IService
{
    public interface INoteService
    {
        public NoteEntity1 AddNote(NoteModel noteModel, int createdBY);
        public List<NoteEntity1> GetNotes(int userId);

        public bool EditNote(int noteId, int userId, NoteModel model);

        public bool DeleteNote(int noteId, int userId);
    }
}
