﻿namespace NoteRepository.Common.Utility.Validation
{
    public interface IValidator<in T> where T : class
    {
        /// <summary>
        /// Gets or sets the validation error.
        /// </summary>
        /// <value>
        /// The validation error.
        /// </value>
        string ValidationError { get; }

        /// <summary>
        /// Determines whether a domain entity is valid.
        /// </summary>
        /// <param name="entity">The domain entity to be checked.</param>
        /// <param name="isNewEntity"><c>True</c> if checking a new domain entity before save it to database</param>
        /// <returns><c>True</c> if the domain entity is valid, otherwise <c>False</c> and <see cref="ValidationError"/> will be set with validation error</returns>
        bool IsValid(T entity, bool isNewEntity);
    }
}