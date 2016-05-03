using NoteRepository.Common.Utility.Dal;
using System;

namespace NoteRepository.Common.Utility.Validation
{
    public abstract class ValidatorBase<T> : IValidator<T> where T : class
    {
        #region constructor

        protected ValidatorBase(ILookupRepository lookupRepo)
        {
            Guard.Against<ArgumentNullException>(lookupRepo == null, "lookupRepo");
            LookupRepo = lookupRepo;
        }

        #endregion constructor

        #region protected properties

        protected ILookupRepository LookupRepo { get; private set; }

        #endregion protected properties

        public string ValidationError { get; protected set; }

        public abstract bool IsValid(T entity, bool isNewEntity);
    }
}