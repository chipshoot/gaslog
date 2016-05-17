using NoteRepository.Common.Utility.Dal;
using NoteRepository.Dal.NH;
using NoteRepository.Core.DomainEntity;
using NUnit.Framework;
using System.Linq;

namespace NoteRepository.Dal.Tests.MappingTests
{
    [TestFixture]
    public class NoteRenderMapTests : TestFixtureBase
    {
        #region private fields

        private IRepository<NoteRender> _renderRepository;
        private ILookupRepository _lookup;

        #endregion private fields

        #region setup environment

        [SetUp]
        public void SetupEnvironment()
        {
            // Arrange - create repository
            _renderRepository = new NhRepository<NoteRender>(UnitOfWorkTest);
            _lookup = new NhLookupRepository(UnitOfWorkTest);
        }

        #endregion setup environment

        #region tests

        [Test]
        public void Can_Get_NoteRender_With_Valid_Id()
        {
            // Act
            var render = _renderRepository.FindEntities().FirstOrDefault(c => c.Id == 1);

            // Assert
            Assert.NotNull(render);
            Assert.That(render.Id, Is.EqualTo(1));
            Assert.That(render.Name, Is.EqualTo("Default"));
            Assert.That(render.NameSpace, Is.EqualTo("NoteRepository.Render"));
            Assert.That(render.Description, Is.EqualTo("The default render of all note"));
        }

        [Test]
        public void Get_NoteRender_With_Invalid_Id_Null_Returned()
        {
            // Act
            var render = _renderRepository.FindEntities().FirstOrDefault(c => c.Id == 0);

            // Assert
            Assert.IsNull(render);
        }

        [Test]
        public void Get_NoteRender_With_No_Existed_Id_Null_Retured()
        {
            // Act
            var render = _renderRepository.FindEntities().FirstOrDefault(c => c.Id == 9898);

            // Assert
            Assert.IsNull(render);
        }

        [Test]
        public void Can_Save_New_NoteRender_With_ValidInfo()
        {
            // Arrange
            var newRender = new NoteRender
            {
                Name = "GasLogRender",
                NameSpace = ("NoteRepository.Render"),
                Description = "The render of gas log"
            };

            // Act
            _renderRepository.Add(newRender);
            var addedRender = _renderRepository.FindEntities().FirstOrDefault(u => u.Name == "GasLogRender");

            // Assert
            Assert.NotNull(addedRender);
            Assert.That(addedRender.Name, Is.EqualTo("GasLogRender"));
            Assert.That(addedRender.NameSpace, Is.EqualTo("NoteRepository.Render"));

            // Tear down changes
            _renderRepository.Delete(newRender);
        }

        [Test]
        public void Can_Delete_Saved_NoteRender()
        {
            // arrange
            var newRender = new NoteRender
            {
                Name = "GasLog",
                NameSpace = "NoteRepository.Render",
                Description = "The render of gas log"
            };

            _renderRepository.Add(newRender);

            var addedRender = _renderRepository.FindEntities().FirstOrDefault(u => u.Name == "GasLog");
            Assert.NotNull(addedRender);

            // act
            _renderRepository.Delete(addedRender);

            // assert
            addedRender = _renderRepository.FindEntities().FirstOrDefault(u => u.Name == "GasLog");
            Assert.Null(addedRender);
        }

        #endregion tests
    }
}