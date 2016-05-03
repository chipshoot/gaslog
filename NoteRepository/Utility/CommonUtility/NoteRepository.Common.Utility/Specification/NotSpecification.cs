using System;
using NoteRepository.Common.Utility.Validation;

namespace NoteRepository.Common.Utility.Specification
{
    internal class NotSpecification<TEntity> : ISpecification<TEntity>
    {
        private readonly ISpecification<TEntity> _wrapped;

        protected ISpecification<TEntity> Wrapped
        {
            get
            {
                return _wrapped;
            }
        }

        internal NotSpecification(ISpecification<TEntity> spec)
        {
            Guard.Against<ArgumentNullException>(spec == null, "spec");

            _wrapped = spec;
        }

        public bool IsSatisfiedBy(TEntity candidate)
        {
            return !Wrapped.IsSatisfiedBy(candidate);
        }
    }
}