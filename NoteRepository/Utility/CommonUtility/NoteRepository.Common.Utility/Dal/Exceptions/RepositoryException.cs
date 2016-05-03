using System;
using System.Runtime.Serialization;

namespace NoteRepository.Common.Utility.Dal.Exceptions
{
    /// <summary>
    /// Class RepositoryException is thrown whenever there is any error occurred
    /// during processing data throw repository
    /// </summary>
    [Serializable]
    public class RepositoryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException" /> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public RepositoryException(string errorMessage)
            : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException" /> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public RepositoryException(string errorMessage, System.Exception innerException)
            : base(errorMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException" /> class.
        /// </summary>
        /// <param name="si">The serialization information.</param>
        /// <param name="sc">The streaming context.</param>
        protected RepositoryException(SerializationInfo si, StreamingContext sc)
            : base(si, sc)
        {
        }
    }
}