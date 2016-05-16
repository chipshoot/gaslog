using NoteRepository.Common.Utility.Dal;
using NoteRepository.Dal.NH;
using NoteRepository.DomainEntity;
using NUnit.Framework;
using System.Linq;

namespace NoteRepository.Dal.Tests.MappingTests
{
    [TestFixture]
    public class NoteCatalogMapTests : TestFixtureBase
    {
        #region private fields

        private IRepository<NoteCatalog> _noteCatalogRepository;
        private ILookupRepository _lookup;

        #endregion private fields

        #region setup environment

        [SetUp]
        public void SetupEnvironment()
        {
            // Arrange - create repository
            _noteCatalogRepository = new NhRepository<NoteCatalog>(UnitOfWorkTest);
            _lookup = new NhLookupRepository(UnitOfWorkTest);
        }

        #endregion setup environment

        #region tests

        [Test]
        public void Can_Get_NoteCatalog_With_Valid_Id()
        {
            // Act
            var noteCatalog = _noteCatalogRepository.FindEntities().FirstOrDefault(c => c.Id == 1);

            // Assert
            Assert.NotNull(noteCatalog);
            Assert.That(noteCatalog.Id, Is.EqualTo(1));
            Assert.That(noteCatalog.Name, Is.EqualTo("GasLog"));
            Assert.That(noteCatalog.Description, Is.EqualTo("The log item of gas adding"));
        }

        [Test]
        public void Get_NoteCatalog_With_Invalid_Id_Null_Returned()
        {
            // Act
            var noteCatalog = _noteCatalogRepository.FindEntities().FirstOrDefault(c => c.Id == 0);

            // Assert
            Assert.IsNull(noteCatalog);
        }

        [Test]
        public void Get_NoteCatalog_With_No_Existed_Id_Null_Retured()
        {
            // Act
            var noteCatalog = _noteCatalogRepository.FindEntities().FirstOrDefault(c => c.Id == 9898);

            // Assert
            Assert.IsNull(noteCatalog);
        }

        [Test]
        public void Can_Save_New_NoteCatalog_With_ValidInfo()
        {
            // Arrange
            var newNoteCatalog = new NoteCatalog
            {
                Name = "Diary",
                Description = "The diary of each day"
            };

            // Act
            _noteCatalogRepository.Add(newNoteCatalog);
            var addedNoteCatalog = _noteCatalogRepository.FindEntities().FirstOrDefault(u => u.Name == "Diary");

            // Assert
            Assert.NotNull(addedNoteCatalog);
            Assert.That(addedNoteCatalog.Name, Is.EqualTo("Diary"));
            Assert.That(addedNoteCatalog.Description, Is.EqualTo("The diary of each day"));

            // Tear down changes
            _noteCatalogRepository.Delete(newNoteCatalog);
        }

        [Test]
        public void Can_Delete_Saved_NoteCatalog()
        {
            // arrange
            var newNoteCatalog = new NoteCatalog
            {
                Name = "Diary",
                Description = "The diary of each day"
            };

            _noteCatalogRepository.Add(newNoteCatalog);

            var addedNoteCatalog = _noteCatalogRepository.FindEntities().FirstOrDefault(u => u.Name == "Diary");
            Assert.NotNull(addedNoteCatalog);

            // act
            _noteCatalogRepository.Delete(addedNoteCatalog);

            // assert
            addedNoteCatalog = _noteCatalogRepository.FindEntities().FirstOrDefault(u => u.Name == "Diary");
            Assert.Null(addedNoteCatalog);
        }

        #endregion tests
    }
}