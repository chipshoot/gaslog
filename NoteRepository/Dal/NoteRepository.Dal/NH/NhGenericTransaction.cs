using System;
using NoteRepository.Common.Utility.Dal;
using NoteRepository.Common.Utility.Dal.Exceptions;
using NoteRepository.Common.Utility.Validation;
using NHibernate;

namespace NoteRepository.Dal.NH
{
    public class NhGenericTransaction : IGenericTransaction
    {
        #region private fields

        private readonly ITransaction _transaction;

        #endregion private fields

        #region constructor

        public NhGenericTransaction(ITransaction transaction)
        {
            Guard.Against<ArgumentNullException>(transaction == null, "transaction");
            _transaction = transaction;
        }

        #endregion constructor

        #region implementation of interface IGenericTransaction

        public void Commit()
        {
            Guard.Against<UnitOfWorkException>(_transaction == null, "The current transaction has not been initialized.");

            // ReSharper disable once PossibleNullReferenceException
            if (!_transaction.IsActive)
            {
                throw new UnitOfWorkException("No active transaction");
            }

            _transaction.Commit();
        }

        public void Rollback()
        {
            Guard.Against<UnitOfWorkException>(_transaction == null, "The current transaction has not been initialized.");

            // ReSharper disable once PossibleNullReferenceException
            if (_transaction.IsActive)
            {
                _transaction.Rollback();
            }
        }

        #endregion implementation of interface IGenericTransaction

        #region implementation of interface IDisposable

        public void Dispose()
        {
            _transaction.Dispose();
        }

        #endregion implementation of interface IDisposable
    }
}