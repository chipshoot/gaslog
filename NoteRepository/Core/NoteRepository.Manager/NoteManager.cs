using NoteRepository.Common.Utility.Dal;
using NoteRepository.Common.Utility.Validation;
using NoteRepository.Core.DomainEntity;
using NoteRepository.Core.Manager.Contract.NoteManager;
using System;

namespace NoteRepository.Core.Manager
{
    public class NoteManager : INoteManager
    {
        #region private fields

        private IRepository<Note> _noteRepo;
        private IValidator<Note> _noteValidator;

        #endregion private fields

        #region constructors

        public NoteManager(IRepository<Note> noteRepo, IValidator<Note> validator)
        {
            Guard.Against<ArgumentNullException>(noteRepo == null, nameof(noteRepo));
            Guard.Against<ArgumentNullException>(validator == null, nameof(validator));

            _noteRepo = noteRepo;
            _noteValidator = validator;
        }

        #endregion constructors

        #region implementation of interface INoteManager

        public string ErrorMessage { get; private set; }

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
            if (!_noteValidator.IsValid(note, true))
            {
                if (note.Catalog == null)
                {
                }
                ErrorMessage = _noteValidator.ValidationError;
                return null;
            }

            note.CreateDate = DateTime.Now;
            note.LastModifiedDate = DateTime.Now;
            return _noteRepo.Add(note);
        }

        #endregion implementation of interface INoteManager
    }
}