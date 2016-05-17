using NoteRepository.Common.Utility.Dal;
using NoteRepository.Dal.NH;
using NoteRepository.Core.DomainEntity;
using NUnit.Framework;
using System.Linq;

namespace NoteRepository.Dal.Tests.MappingTests
{
    [TestFixture]
    public class TagMapTests : TestFixtureBase
    {
        #region private fields

        private IRepository<Tag> _tagRepository;

        #endregion private fields

        #region setup environment

        [SetUp]
        public void SetupEnvironment()
        {
            // Arrange - create repository
            _tagRepository = new NhRepository<Tag>(UnitOfWorkTest);
        }

        #endregion setup environment

        #region tests

        [Test]
        public void Can_Get_Tag_With_Valid_Id()
        {
            // Act
            var tag = _tagRepository.FindEntities().FirstOrDefault(c => c.Id == 1);

            // Assert
            Assert.NotNull(tag);
            Assert.That(tag.Id, Is.EqualTo(1));
            Assert.That(tag.Name, Is.EqualTo("Default Gas"));
            Assert.That(tag.IsActivated, Is.True);
        }

        [Test]
        public void Get_Tag_With_Invalid_Id_Null_Returned()
        {
            // Act
            var tag = _tagRepository.FindEntities().FirstOrDefault(c => c.Id == 0);

            // Assert
            Assert.IsNull(tag);
        }

        [Test]
        public void Get_Tag_With_No_Existed_Id_Null_Retured()
        {
            // Act
            var tag = _tagRepository.FindEntities().FirstOrDefault(c => c.Id == 9898);

            // Assert
            Assert.IsNull(tag);
        }

        [Test]
        public void Can_Save_New_Tag_With_ValidInfo()
        {
            // Arrange
            var newTag = new Tag
            {
                Name = "Weather",
                IsActivated = true,
                Description = "Weather not good"
            };

            // Act
            _tagRepository.Add(newTag);
            var addedTag = _tagRepository.FindEntities().FirstOrDefault(u => u.Name == "Weather");

            // Assert
            Assert.NotNull(addedTag);
            Assert.That(addedTag.Name, Is.EqualTo("Weather"));
            Assert.That(addedTag.IsActivated, Is.True);

            // Tear down changes
            _tagRepository.Delete(newTag);
        }

        [Test]
        public void Can_Delete_Saved_Tag()
        {
            // arrange
            var newTag = new Tag
            {
                Name = "Weather",
                IsActivated = true,
                Description = "Weather not good"
            };

            _tagRepository.Add(newTag);

            var addedTag = _tagRepository.FindEntities().FirstOrDefault(u => u.Name == "Weather");
            Assert.NotNull(addedTag);

            // act
            _tagRepository.Delete(addedTag);

            // assert
            addedTag = _tagRepository.FindEntities().FirstOrDefault(u => u.Name == "Weather");
            Assert.Null(addedTag);
        }

        #endregion tests
    }
}