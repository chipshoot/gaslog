using NoteRepository.Core.DomainEntity;

namespace NoteRepository.Core.Manager.Contract.NoteManager
{
    public interface INoteManager
    {
        Note GetNoteById(int id);

        Note CreateNewNote(Note note);

        Note UpdateNote(Note note);

        bool DeleteNote(int id);
    }
}