using NHibernate.Context;
using NoteRepository.Common.Utility.Dal;
using NoteRepository.Common.Utility.Dal.Exceptions;
using NoteRepository.Common.Utility.Validation;
using System;

namespace NoteRepository.Dal.NH.Infrastructure.SessionMan
{
    public class NhUnitOfWorkFactory : IUnitOfWorkFactory
    {
        #region private fields

        private readonly ISessionFactoryProvider _factoryProvider;

        #endregion private fields

        #region constructor

        public NhUnitOfWorkFactory(ISessionFactoryProvider factoryProvider)
        {
            Guard.Against<ArgumentNullException>(factoryProvider == null, "factoryProvider");
            _factoryProvider = factoryProvider;
        }

        #endregion constructor

        #region implementation of interface IUnitOfWorkFactory

        /// <summary>
        /// Gets the current <see cref="IUnitOfWork"/> which against database.
        /// </summary>
        /// <value>
        /// The current connected <see cref="IUnitOfWork"/>.
        /// </value>
        public IUnitOfWork CurrentUnitOfWork => CreateUnitOfWork();

        /// <summary>
        /// Binds the unit of work in certain context for MVC and WCF web services.
        /// </summary>
        public void BindUnitOfWork()
        {
            var factory = _factoryProvider.GetFactory();
            if (CurrentSessionContext.HasBind(factory))
            {
                return;
            }

            var session = factory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        /// <summary>
        /// Closes the unit of work.
        /// </summary>
        public void CloseUnitOfWork()
        {
            var factory = _factoryProvider.GetFactory();
            if (factory == null)
            {
                return;
            }

            if (!CurrentSessionContext.HasBind(factory))
            {
                return;
            }

            CurrentSessionContext.Unbind(factory);
        }

        #endregion implementation of interface IUnitOfWorkFactory

        #region private methods

        /// <summary>
        /// Creates the unit of work based on current NHibernate session.
        /// <remarks>
        /// Because <see cref="NhUnitOfWork"/> is only a thin wrap of NHibernate session, so it's okay
        /// for us to create a brand new unit of work each time
        /// </remarks>
        /// </summary>
        /// <returns>The new created unit of work which contains session in current context</returns>
        private IUnitOfWork CreateUnitOfWork()
        {
            // setup session factory which connect to database
            var factory = _factoryProvider.GetFactory();
            if (factory == null)
            {
                var errorMsg = "Cannot find session factory";
                throw new UnitOfWorkException(errorMsg);
            }

            var session = CurrentSessionContext.HasBind(factory) ? factory.GetCurrentSession() : factory.OpenSession();
            var quickSession = factory.OpenStatelessSession();
            var uow = new NhUnitOfWork(session, quickSession);

            return uow;
        }

        #endregion private methods
    }
}