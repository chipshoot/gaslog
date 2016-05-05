using NHibernate;
using System;

namespace NoteRepository.Dal.NH.Infrastructure.SessionMan
{
    public interface ISessionFactoryProvider : IDisposable
    {
        /// <summary>
        /// Occurs just before close session factory and you can do also resource release at this time.
        /// </summary>
        event EventHandler<EventArgs> BeforeCloseSessionFactory;

        /// <summary>
        /// Gets the session factory.
        /// </summary>
        /// <returns>The session factory</returns>
        ISessionFactory GetFactory();
    }
}