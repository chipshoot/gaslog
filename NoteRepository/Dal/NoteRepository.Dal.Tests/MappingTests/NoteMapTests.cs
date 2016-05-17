using System;
using System.Linq;
using NoteRepository.Common.Utility.Dal;
using NoteRepository.Dal.NH;
using NoteRepository.Core.DomainEntity;
using NUnit.Framework;

namespace NoteRepository.Dal.Tests.MappingTests
{
    [TestFixture]
    public class NoteMapTests : TestFixtureBase
    {
        #region private fields

        private IRepository<Note> _noteRepository;
        private ILookupRepository _lookup;

        #endregion private fields

        #region setup environment

        [SetUp]
        public void SetupEnvironment()
        {
            // Arrange - create repository
            _noteRepository = new NhRepository<Note>(UnitOfWorkTest);
            _lookup = new NhLookupRepository(UnitOfWorkTest);
        }

        #endregion setup environment

        #region tests

        [Test]
        public void Can_Get_Note_With_Valid_Id()
        {
            // Act
            var note = _noteRepository.FindEntities().FirstOrDefault(c => c.Id == 2);

            // Assert
            Assert.NotNull(note);
            Assert.That(note.Id, Is.EqualTo(2));
            Assert.That(note.Subject, Is.EqualTo("First test Note"));
            Assert.That(note.Catalog.Name, Is.EqualTo("GasLog"));
            Assert.That(note.Render.Name, Is.EqualTo("Default"));
            Assert.That(note.Author.FirstName, Is.EqualTo("Jack"));
        }

        [Test]
        public void Get_Note_With_Invalid_Id_Null_Returned()
        {
            // Act
            var note = _noteRepository.FindEntities().FirstOrDefault(c => c.Id == 0);

            // Assert
            Assert.IsNull(note);
        }

        [Test]
        public void Get_Note_WithNoExisted_Id_NullRetured()
        {
            // Act
            var note = _noteRepository.FindEntities().FirstOrDefault(c => c.Id == 9898);

            // Assert
            Assert.IsNull(note);
        }

        [Test]
        public void Can_Save_New_Note_With_ValidInfo()
        {
            // Arrange
            var newNote = new Note
            {
                Subject = "This is a first gas log",
                Content = "Content",
                Catalog = _lookup.FindEntities<NoteCatalog>().FirstOrDefault(c => c.Name == "GasLog"),
                Render = _lookup.FindEntities<NoteRender>().FirstOrDefault(r=>r.Name=="Default"),
                Author = _lookup.FindEntities<User>().FirstOrDefault(u => u.AccountName == "jfang"),
                CreateDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Description = "This is only used for working in Hadrian Manufacturing Inc."
            };

            // Act
            _noteRepository.Add(newNote);
            var addedNote = _noteRepository.FindEntities().FirstOrDefault(u => u.Content == "Content");

            // Assert
            Assert.NotNull(addedNote);
            Assert.That(addedNote.Content, Is.EqualTo("Content"));
            Assert.That(addedNote.Catalog.Name, Is.EqualTo("GasLog"));

            // Tear down changes
            _noteRepository.Delete(newNote);
        }

        [Test]
        public void Can_Delete_Saved_Note()
        {
            // arrange
            var newNote = new Note
            {
                Subject = "This is a first gas log",
                Content = "Content",
                Catalog = _lookup.FindEntities<NoteCatalog>().FirstOrDefault(c => c.Name == "GasLog"),
                Render = _lookup.FindEntities<NoteRender>().FirstOrDefault(r=>r.Name=="Default"),
                Author = _lookup.FindEntities<User>().FirstOrDefault(u => u.AccountName == "jfang"),
                CreateDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Description = "This is only used for working in Hadrian Manufacturing Inc."
            };

            _noteRepository.Add(newNote);

            var addedNote = _noteRepository.FindEntities().FirstOrDefault(u => u.Content == "Content");
            Assert.NotNull(addedNote);

            // act
            _noteRepository.Delete(addedNote);

            // assert
            addedNote = _noteRepository.FindEntities().FirstOrDefault(u => u.Content == "Content");
            Assert.Null(addedNote);
        }

        #endregion tests
    }
}