using Moq;
using NHibernate;
using NoteRepository.Dal.NH;
using NUnit.Framework;

namespace NoteRepository.Dal.Tests
{
    [TestFixture]
    public class NhGenericTransactionTests
    {
        [Test]
        public void Can_Dispose_After_Using_Finished()
        {
            var nhTrans = new Mock<ITransaction>().Object;
            var sessionMock = new Mock<ISession>();
            var quicksessionMock = new Mock<IStatelessSession>();
            sessionMock.Setup(s => s.BeginTransaction()).Returns(nhTrans);
            var session = sessionMock.Object;
            var uow = new NhUnitOfWork(session, quicksessionMock.Object);

            using (var trans = uow.BeginTransaction())
            {
            }
        }
    }
}