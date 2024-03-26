using Note_Microservices.Model;
using Note_Microservices.NoteEntity;
using NuGet.Common;

namespace Note_Microservices.IService
{
    public interface INoteService
    {
        public Task<NoteEntity1> AddNote(NoteModel noteModel, int createdBY, string token);
        public List<NoteEntity1> GetNotes(int userId);

        public bool EditNote(int noteId, int userId, NoteModel model);

        public bool DeleteNote(int noteId, int userId);
    }
}
