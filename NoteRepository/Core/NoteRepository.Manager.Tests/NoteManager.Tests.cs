using NoteRepository.Common.TestHelper;
using NoteRepository.Common.Utility.Dal;
using NoteRepository.Core.DomainEntity;
using NoteRepository.Core.Manager;
using NoteRepository.Core.Manager.Contract.NoteManager;
using NoteRepository.Core.Manager.Validatior;
using NUnit.Framework;
using System;
using System.Linq;

namespace NoteRepository.Manager.Tests
{
    [TestFixture]
    public class NoteManagerTests
    {
        #region private fields

        private IRepository<Note> _noteRepo;
        private INoteManager _manager;
        private ILookupRepository _lookupRepo;

        #endregion private fields

        #region constructors

        [SetUp]
        public void SetEnvirnonment()
        {
            _lookupRepo = SetupDataSource.LookupRepository;
            _noteRepo = SetupDataSource.NoteRepo;
            var validator = new NoteValidator(_lookupRepo);
            _manager = new NoteManager(_noteRepo, validator);
        }

        #endregion constructors

        #region tests

        [Test]
        public void Create_Note_With_Valid_Record_With_New_Id_Returned_Without_ErrorMessage_Record_Added()
        {
            // Arrange
            var note = new Note
            {
                Subject = "a new added note",
                Content = "Just a sample note for testing",
                Author = _lookupRepo.FindEntities<User>().FirstOrDefault(u => u.Id == 1),
                Catalog = _lookupRepo.FindEntities<NoteCatalog>().FirstOrDefault(c => c.Id == 1),
                Render = _lookupRepo.FindEntities<NoteRender>().FirstOrDefault(r => r.Id == 1),
            };
            var notec = _lookupRepo.FindEntities<Note>().Count();

            // Act
            var newNote = _manager.CreateNewNote(note);
            var notenc = _lookupRepo.FindEntities<Note>().Count();

            // Assert
            Assert.NotNull(newNote);
            Assert.That(newNote.Id, Is.GreaterThan(0));
            Assert.That(notenc, Is.EqualTo(notec + 1));
            Assert.That(note.CreateDate.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(note.LastModifiedDate.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Create_Note_With_Null_Return_Null_With_ErrorMsg_No_Record_Added()
        {
            // Arrange
            var notec = _lookupRepo.FindEntities<Note>().Count();

            // Act
            var newNote = _manager.CreateNewNote(null);
            var notenc = _lookupRepo.FindEntities<Note>().Count();

            // Assert
            Assert.Null(newNote);
            Assert.That(_manager.ErrorMessage, Is.Not.Empty);
            Assert.That(notenc, Is.EqualTo(notec));
        }

        [Test]
        public void Create_Note_With_Invalid_Author_Return_Null_With_ErrorMsg()
        {
            // Arrange - null Author
            var note = new Note
            {
                Subject = "a new added note",
                Content = "Just a sample note for testing",
                Author = null,
                Catalog = _lookupRepo.FindEntities<NoteCatalog>().FirstOrDefault(c => c.Id == 1),
                Render = _lookupRepo.FindEntities<NoteRender>().FirstOrDefault(r => r.Id == 1),
            };
            var notec = _lookupRepo.FindEntities<Note>().Count();

            // Act
            var newNote = _manager.CreateNewNote(note);
            var notenc = _lookupRepo.FindEntities<Note>().Count();

            // Assert
            Assert.Null(newNote);
            Assert.That(_manager.ErrorMessage, Is.Not.Empty);
            Assert.That(notenc, Is.EqualTo(notec));

            // Arrange - not existed Author
            note.Author = new User();
            notec = _lookupRepo.FindEntities<Note>().Count();

            // Act
            newNote = _manager.CreateNewNote(note);
            notenc = _lookupRepo.FindEntities<Note>().Count();

            // Assert
            Assert.Null(newNote);
            Assert.That(_manager.ErrorMessage, Is.Not.Empty);
            Assert.That(notenc, Is.EqualTo(notec));
        }

        [Test]
        public void Create_Note_With_Invalid_Catalog_Successed_Added_With_Default_Catalog_Select()
        {
            // Arrange - null Author
            var note = new Note
            {
                Subject = "a new added note",
                Content = "Just a sample note for testing",
                Author = _lookupRepo.FindEntities<User>().FirstOrDefault(u => u.Id == 1),
                Catalog = null,
                Render = _lookupRepo.FindEntities<NoteRender>().FirstOrDefault(r => r.Id == 1),
            };
            var notec = _lookupRepo.FindEntities<Note>().Count();

            // Act
            var newNote = _manager.CreateNewNote(note);
            var notenc = _lookupRepo.FindEntities<Note>().Count();

            // Assert
            Assert.NotNull(newNote);
            Assert.That(_manager.ErrorMessage, Is.Not.Empty);
            Assert.That(notenc, Is.EqualTo(notec + 1));

            // Arrange - not existed Author
            note.Catalog = new NoteCatalog();
            notec = _lookupRepo.FindEntities<Note>().Count();

            // Act
            newNote = _manager.CreateNewNote(note);
            notenc = _lookupRepo.FindEntities<Note>().Count();

            // Assert
            Assert.NotNull(newNote);
            Assert.That(_manager.ErrorMessage, Is.Not.Empty);
            Assert.That(notenc, Is.EqualTo(notec + 1));
        }

        [Test]
        public void Create_Note_With_Invalid_Render_Create_Successed_With_Default_Render()
        {
        }

        #endregion tests
    }
}