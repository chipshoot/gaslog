using NoteRepository.Common.Utility.Dal;
using NoteRepository.Dal.NH;
using NoteRepository.DomainEntity;
using NUnit.Framework;
using System.Linq;

namespace NoteRepository.Dal.Tests.MappingTests
{
    [TestFixture]
    public class PhoneMapTests : TestFixtureBase
    {
        #region private fields

        private IRepository<Phone> _phoneRepository;
        private ILookupRepository _lookup;

        #endregion private fields

        #region setup environment

        [SetUp]
        public void SetupEnvironment()
        {
            // Arrange - create repository
            _phoneRepository = new NhRepository<Phone>(UnitOfWorkTest);
            _lookup = new NhLookupRepository(UnitOfWorkTest);
        }

        #endregion setup environment

        #region tests

        [Test]
        public void Can_Get_Phone_With_Valid_Id()
        {
            // Act
            var phone = _phoneRepository.FindEntities().FirstOrDefault(c => c.Id == 1);

            // Assert
            Assert.NotNull(phone);
            Assert.That(phone.Id, Is.EqualTo(1));
            Assert.That(phone.AreaCode, Is.EqualTo(1));
            Assert.That(phone.Number, Is.EqualTo("905-333-0300"));
            Assert.That(phone.Extension, Is.EqualTo(2058));
            Assert.That(phone.Catalog.Name, Is.EqualTo("Work Phone"));
            Assert.That(phone.Owner.FirstName, Is.EqualTo("Jack"));
        }

        [Test]
        public void Get_Phone_With_Invalid_Id_Null_Returned()
        {
            // Act
            var phone = _phoneRepository.FindEntities().FirstOrDefault(c => c.Id == 0);

            // Assert
            Assert.IsNull(phone);
        }

        [Test]
        public void Get_Phone_WithNoExisted_Id_Null_Retured()
        {
            // Act
            var phone = _phoneRepository.FindEntities().FirstOrDefault(c => c.Id == 9898);

            // Assert
            Assert.IsNull(phone);
        }

        [Test]
        public void Can_Save_New_Phone_With_ValidInfo()
        {
            // Arrange
            var newPhone = new Phone
            {
                AreaCode = 1,
                Country = "CA",
                Number = "905-206-9886",
                Catalog = _lookup.FindEntities<ContactInfoCatalog>().FirstOrDefault(c => c.Name == "Work Phone"),
                Owner = _lookup.FindEntities<User>().FirstOrDefault(u => u.AccountName == "jfang"),
                IsActivated = true,
                Description = "The phone is a home phone"
            };

            // Act
            _phoneRepository.Add(newPhone);
            var addedPhone = _phoneRepository.FindEntities().FirstOrDefault(u => u.Number == "905-206-9886");

            // Assert
            Assert.NotNull(addedPhone);
            Assert.That(addedPhone.Number, Is.EqualTo("905-206-9886"));
            Assert.That(addedPhone.Catalog.Name, Is.EqualTo("Work Phone"));

            // Tear down changes
            _phoneRepository.Delete(newPhone);
        }

        [Test]
        public void Can_Delete_Saved_Phone()
        {
            // arrange
            var newPhone = new Phone
            {
                AreaCode = 1,
                Country = "CA",
                Number = "905-206-9886",
                Catalog = _lookup.FindEntities<ContactInfoCatalog>().FirstOrDefault(c => c.Name == "Work Phone"),
                Owner = _lookup.FindEntities<User>().FirstOrDefault(u => u.AccountName == "jfang"),
                IsActivated = true,
                Description = "The phone is a home phone"
            };

            _phoneRepository.Add(newPhone);

            var addedPhone = _phoneRepository.FindEntities().FirstOrDefault(u => u.Number == "905-206-9886");
            Assert.NotNull(addedPhone);

            // act
            _phoneRepository.Delete(addedPhone);

            // assert
            addedPhone = _phoneRepository.FindEntities().FirstOrDefault(u => u.Number == "905-206-9886");
            Assert.Null(addedPhone);
        }

        #endregion tests
    }
}