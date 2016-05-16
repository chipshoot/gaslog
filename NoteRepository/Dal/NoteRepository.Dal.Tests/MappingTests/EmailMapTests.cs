using NoteRepository.Common.Utility.Dal;
using NoteRepository.Dal.NH;
using NoteRepository.DomainEntity;
using NUnit.Framework;
using System.Linq;

namespace NoteRepository.Dal.Tests.MappingTests
{
    [TestFixture]
    public class EmailMapTests : TestFixtureBase
    {
        #region private fields

        private IRepository<Email> _emailRepository;
        private ILookupRepository _lookup;

        #endregion private fields

        #region setup environment

        [SetUp]
        public void SetupEnvironment()
        {
            // Arrange - create repository
            _emailRepository = new NhRepository<Email>(UnitOfWorkTest);
            _lookup = new NhLookupRepository(UnitOfWorkTest);
        }

        #endregion setup environment

        #region tests

        [Test]
        public void Can_Get_Email_With_Valid_Id()
        {
            // Act
            var email = _emailRepository.FindEntities().FirstOrDefault(c => c.Id == 2);

            // Assert
            Assert.NotNull(email);
            Assert.That(email.Id, Is.EqualTo(2));
            Assert.That(email.Address, Is.EqualTo("fchy@yahoo.com"));
            Assert.That(email.Catalog.Name, Is.EqualTo("Home"));
            Assert.That(email.Owner.FirstName, Is.EqualTo("Jack"));
        }

        [Test]
        public void Get_Email_With_Invalid_Id_Null_Returned()
        {
            // Act
            var email = _emailRepository.FindEntities().FirstOrDefault(c => c.Id == 0);

            // Assert
            Assert.IsNull(email);
        }

        [Test]
        public void Get_Email_With_No_Existed_Id_Null_Retured()
        {
            // Act
            var email = _emailRepository.FindEntities().FirstOrDefault(c => c.Id == 9898);

            // Assert
            Assert.IsNull(email);
        }

        [Test]
        public void Can_Save_New_Email_With_ValidInfo()
        {
            // Arrange
            var newEmail = new Email
            {
                Address = "jfang@hadrian-inc.com",
                Catalog = _lookup.FindEntities<ContactInfoCatalog>().FirstOrDefault(c=>c.Name=="Work"),
                IsPrimary = false,
                Owner = _lookup.FindEntities<User>().FirstOrDefault(u=>u.AccountName == "jfang"),
                IsActivated = true,
                Description = "The email is only used for working in Hadrian Manufacturing Inc."
            };

            // Act
            _emailRepository.Add(newEmail);
            var addedEmail = _emailRepository.FindEntities().FirstOrDefault(u => u.Address == "jfang@hadrian-inc.com");

            // Assert
            Assert.NotNull(addedEmail);
            Assert.That(addedEmail.Address, Is.EqualTo("jfang@hadrian-inc.com"));
            Assert.That(addedEmail.Catalog.Name, Is.EqualTo("Work"));

            // Tear down changes
            _emailRepository.Delete(newEmail);
        }

        [Test]
        public void Can_Delete_Saved_Email()
        {
            // arrange
            var newEmail = new Email
            {
                Address = "jfang@hadrian-inc.com",
                Catalog = _lookup.FindEntities<ContactInfoCatalog>().FirstOrDefault(c => c.Name == "Work"),
                IsPrimary = false,
                Owner = _lookup.FindEntities<User>().FirstOrDefault(u => u.AccountName == "jfang"),
                IsActivated = true,
                Description = "The email is only used for working in Hadrian Manufacturing Inc."
            };

            _emailRepository.Add(newEmail);

            var addedEmail = _emailRepository.FindEntities().FirstOrDefault(u => u.Address == "jfang@hadrian-inc.com");
            Assert.NotNull(addedEmail);

            // act
            _emailRepository.Delete(addedEmail);

            // assert
            addedEmail = _emailRepository.FindEntities().FirstOrDefault(u => u.Address == "jfang@hadrian-inc.com");
            Assert.Null(addedEmail);
        }

        #endregion tests
    }
}