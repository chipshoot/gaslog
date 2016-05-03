using System;
using System.Runtime.Serialization;

namespace NoteRepository.Common.Utility.Dal.Exceptions
{
    /// <summary>
    /// Class UnitOfWorkException
    /// </summary>
    public class UnitOfWorkException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkException" /> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public UnitOfWorkException(string errorMessage)
            : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkException" /> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public UnitOfWorkException(string errorMessage, System.Exception innerException)
            : base(errorMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkException" /> class.
        /// </summary>
        /// <param name="si">The serialization information.</param>
        /// <param name="sc">The streaming context.</param>
        protected UnitOfWorkException(SerializationInfo si, StreamingContext sc)
            : base(si, sc)
        {
        }
    }
}