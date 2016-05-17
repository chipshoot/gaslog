using NoteRepository.Common.Utility.Dal;
using NoteRepository.Core.DomainEntity;
using NoteRepository.Core.Manager.Contract.NoteManager;
using NUnit.Framework;

namespace NoteRepository.Manager.Tests
{
    [TestFixture]
    public class NoteManagerTests
    {
        #region private fields

        private IRepository<Note> _noteRepo;
        private INoteManager _manager;

        #endregion private fields

        #region constructors

        [SetUp]
        public void SetEnvirnonment()
        {
        }

        #endregion constructors

        #region tests

        [Test]
        public void Create_Note()
        {
        }

        #endregion tests
    }
}