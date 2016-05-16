using NHibernate;
using NHibernate.Exceptions;
using NoteRepository.Common.Utility.Dal;
using NoteRepository.Common.Utility.Dal.Exceptions;
using NoteRepository.Common.Utility.Validation;
using System;

namespace NoteRepository.Dal.NH
{
    /// <summary>
    /// The unit of work which apply NHibernate session
    /// to update database and maintains transactions
    /// </summary>
    public class NhUnitOfWork : IUnitOfWork
    {
        #region private fields

        /// <summary>
        /// The NHibernate session
        /// </summary>
        private readonly ISession _session;

        /// <summary>
        /// The NHibernate stateless session for quick search database
        /// </summary>
        private readonly IStatelessSession _quickSession;

        #endregion private fields

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NhUnitOfWork"/> class.
        /// </summary>
        /// <param name="session">The session instance of NHibernate.</param>
        /// <param name="quickSession">The stateless session instance of NHibernate</param>
        public NhUnitOfWork(ISession session, IStatelessSession quickSession)
        {
            Guard.Against<ArgumentNullException>(session == null, "session");
            Guard.Against<ArgumentNullException>(quickSession == null, "quickSession");

            _session = session;
            _quickSession = quickSession;
        }

        #endregion constructors

        #region public properties

        /// <summary>
        /// Gets the session.
        /// <remarks>
        /// The normal session which is good for
        /// create, finding and updating small set of complex entity
        /// </remarks>
        /// </summary>
        /// <value>The session instance.</value>
        public ISession Session
        {
            get { return _session; }
        }

        /// <summary>
        /// Gets the quick session.
        /// <remarks>
        /// Nhibernate stateless session is good for quick
        /// scan large data set but don't have cache and hard
        /// for eager fetch for repository, so for simple and
        /// quick search or iterate data base, try this session
        /// otherwise use normal session
        /// </remarks>
        /// </summary>
        /// <value>
        /// The quick session which is good for deal with large data set.
        /// </value>
        public IStatelessSession QuickSession
        {
            get { return _quickSession; }
        }

        #endregion public properties

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Dispose()
        {
            _session.Close();
            _session.Dispose();
        }

        #endregion Implementation of IDisposable

        #region Implementation of IUnitOfWork

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <typeparam name="T">the type of item</typeparam>
        /// <param name="entity">The item we want to add.</param>
        /// <returns>The new entity just added to the system</returns>
        /// <exception cref="UnitOfWorkException"></exception>
        public T Add<T>(T entity) where T : class
        {
            using (var trans = BeginTransaction())
            {
                try
                {
                    _session.SaveOrUpdate(entity);
                    trans.Commit();
                    return entity;
                }
                catch (GenericADOException ex)
                {
                    // something wrong in flush, then cancel all changes
                    trans.Rollback();
                    trans.Dispose();
                    _session.Clear();
                    var errMsg = string.Format("An error occurred during the Add method.{1}{0}", ex.Message, Environment.NewLine);
                    throw new UnitOfWorkException(errMsg, ex);
                }
            }
        }

        /// <summary>
        /// Begins a transaction.
        /// </summary>
        /// <returns>The instance of <see cref="IGenericTransaction"/></returns>
        public IGenericTransaction BeginTransaction()
        {
            return new NhGenericTransaction(_session.BeginTransaction());
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <typeparam name="T">the type of the item to be deleted</typeparam>
        /// <param name="entity">The entity which will be deleted.</param>
        /// <exception cref="UnitOfWorkException"></exception>
        public void Delete<T>(T entity) where T : class
        {
            using (var trans = BeginTransaction())
            {
                try
                {
                    _session.Delete(entity);
                    trans.Commit();
                }
                catch (GenericADOException ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    _session.Clear();
                    var errMsg = string.Format("An error occurred during the Add method.{1}{0}", ex.Message, Environment.NewLine);
                    throw new UnitOfWorkException(errMsg, ex);
                }
            }
        }

        /// <summary>
        /// Refreshes the data to database via commit transaction, if something
        /// wrong rollback transaction.
        /// </summary>
        public void Flush()
        {
            using (var trans = BeginTransaction())
            {
                try
                {
                    trans.Commit();
                }
                catch (GenericADOException ex)
                {
                    // something wrong in flush, then cancel all changes
                    trans.Rollback();
                    trans.Dispose();
                    _session.Clear();
                    var errMsg = string.Format("An error occurred during the Flush method.{1}{0}", ex.Message, Environment.NewLine);
                    throw new UnitOfWorkException(errMsg, ex);
                }
            }
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <typeparam name="T">the type of the item to update</typeparam>
        /// <param name="entity">The entity we want to update.</param>
        /// <exception cref="UnitOfWorkException"></exception>
        public void Update<T>(T entity) where T : class
        {
            _session.SaveOrUpdate(entity);
        }

        #endregion Implementation of IUnitOfWork
    }
}