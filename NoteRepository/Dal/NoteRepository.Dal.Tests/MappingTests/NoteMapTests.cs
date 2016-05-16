//using System.Linq;
//using NoteRepository.Common.Utility.Dal;
//using NoteRepository.Dal.NH;
//using NoteRepository.DomainEntity;
//using NUnit.Framework;

//namespace NoteRepository.Dal.Tests.MappingTests
//{
//    [TestFixture]
//    public class NoteMapTests : TestFixtureBase
//    {
//        #region private fields

//        private IRepository<Note> _noteRepository;
//        private ILookupRepository _lookup;

//        #endregion private fields

//        #region setup environment

//        [SetUp]
//        public void SetupEnvironment()
//        {
//            // Arrange - create repository
//            _noteRepository = new NhRepository<Note>(UnitOfWorkTest);
//            _lookup = new NhLookupRepository(UnitOfWorkTest);
//        }

//        #endregion setup environment

//        #region tests

//        [Test]
//        public void Can_Get_NOte_With_Valid_Id()
//        {
//            // Act
//            var note = _noteRepository.FindEntities().FirstOrDefault(c => c.Id == 2);

//            // Assert
//            Assert.NotNull(note);
//            Assert.That(note.Id, Is.EqualTo(2));
//            Assert.That(note.Address, Is.EqualTo("fchy@yahoo.com"));
//            Assert.That(note.Catalog.Name, Is.EqualTo("Home"));
//            Assert.That(note.Owner.FirstName, Is.EqualTo("Jack"));
//        }

//        [Test]
//        public void Get_Email_WithInvalid_Id_NullReturned()
//        {
//            // Act
//            var note = _noteRepository.FindEntities().FirstOrDefault(c => c.Id == 0);

//            // Assert
//            Assert.IsNull(note);
//        }

//        [Test]
//        public void Get_Email_WithNoExisted_Id_NullRetured()
//        {
//            // Act
//            var note = _noteRepository.FindEntities().FirstOrDefault(c => c.Id == 9898);

//            // Assert
//            Assert.IsNull(note);
//        }

//        [Test]
//        public void Can_Save_New_Email_With_ValidInfo()
//        {
//            // Arrange
//            var newNote = new Email
//            {
//                Address = "jfang@hadrian-inc.com",
//                Catalog = _lookup.FindEntities<ContactInfoCatalog>().FirstOrDefault(c => c.Name == "Work"),
//                IsPrimary = false,
//                Owner = _lookup.FindEntities<User>().FirstOrDefault(u => u.AccountName == "jfang"),
//                IsActivated = true,
//                Description = "The email is only used for working in Hadrian Manufacturing Inc."
//            };

//            // Act
//            _noteRepository.Add(newNote);
//            var addedEmail = _noteRepository.FindEntities().FirstOrDefault(u => u.Address == "jfang@hadrian-inc.com");

//            // Assert
//            Assert.NotNull(addedEmail);
//            Assert.That(addedEmail.Address, Is.EqualTo("jfang@hadrian-inc.com"));
//            Assert.That(addedEmail.Catalog.Name, Is.EqualTo("Work"));

//            // Tear down changes
//            _noteRepository.Delete(newNote);
//        }

//        [Test]
//        public void Can_Delete_Saved_Email()
//        {
//            // arrange
//            var newNote = new Email
//            {
//                Address = "jfang@hadrian-inc.com",
//                Catalog = _lookup.FindEntities<ContactInfoCatalog>().FirstOrDefault(c => c.Name == "Work"),
//                IsPrimary = false,
//                Owner = _lookup.FindEntities<User>().FirstOrDefault(u => u.AccountName == "jfang"),
//                IsActivated = true,
//                Description = "The email is only used for working in Hadrian Manufacturing Inc."
//            };

//            _noteRepository.Add(newNote);

//            var addedEmail = _noteRepository.FindEntities().FirstOrDefault(u => u.Address == "jfang@hadrian-inc.com");
//            Assert.NotNull(addedEmail);

//            // act
//            _noteRepository.Delete(addedEmail);

//            // assert
//            addedEmail = _noteRepository.FindEntities().FirstOrDefault(u => u.Address == "jfang@hadrian-inc.com");
//            Assert.Null(addedEmail);
//        }

//        #endregion tests
//    }
//}