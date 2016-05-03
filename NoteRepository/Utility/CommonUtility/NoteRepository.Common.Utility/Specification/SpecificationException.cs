using System;
using System.Runtime.Serialization;

namespace NoteRepository.Common.Utility.Specification
{
    [Serializable]
    public class SpecificationException : ApplicationException
    {/// <summary>
        /// Initializes a new instance of the <see cref="SpecificationException" /> class.
        /// </summary>
        public SpecificationException()
        {
        }
        

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificationException" /> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public SpecificationException(string errorMessage)
            : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificationException" /> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SpecificationException(string errorMessage, System.Exception innerException)
            : base(errorMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificationException" /> class.
        /// </summary>
        /// <param name="si">The serialization information.</param>
        /// <param name="sc">The streaming context.</param>
        protected SpecificationException(SerializationInfo si, StreamingContext sc)
            : base(si, sc)
        {
        }
    }
}