using NoteRepository.Core.DomainEntity;
using NoteRepository.Core.Manager.Contract.NoteManager;

namespace NoteRepository.Core.Manager
{
    public class NoteManager : INoteManager
    {
        public Note GetNoteById(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteNote(int id)
        {
            throw new System.NotImplementedException();
        }

        public Note UpdateNote(Note note)
        {
            throw new System.NotImplementedException();
        }

        public Note CreateNewNote(Note note)
        {
            throw new System.NotImplementedException();
        }
    }
}