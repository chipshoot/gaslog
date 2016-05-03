using System;
using NoteRepository.Common.Utility.Validation;

namespace NoteRepository.Common.Utility.Specification
{
    internal class AndSpecification<TEntity> : ISpecification<TEntity>
    {
        private readonly ISpecification<TEntity> _spec1;
        private readonly ISpecification<TEntity> _spec2;

        protected ISpecification<TEntity> Spec1
        {
            get
            {
                return _spec1;
            }
        }

        protected ISpecification<TEntity> Spec2
        {
            get
            {
                return _spec2;
            }
        }

        internal AndSpecification(ISpecification<TEntity> spec1, ISpecification<TEntity> spec2)
        {
            Guard.Against<ArgumentNullException>(spec1 == null, "spec1");
            Guard.Against<ArgumentNullException>(spec2 == null, "spec2");

            _spec1 = spec1;
            _spec2 = spec2;
        }

        public bool IsSatisfiedBy(TEntity candidate)
        {
            return Spec1.IsSatisfiedBy(candidate) && Spec2.IsSatisfiedBy(candidate);
        }
    }
}