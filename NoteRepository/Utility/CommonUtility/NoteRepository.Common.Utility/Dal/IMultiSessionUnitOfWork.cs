using System.Collections;
using System.Collections.Generic;

namespace NoteRepository.Common.Utility.Dal
{
    /// <summary>
    /// The unit of work hold multiple session which connect
    /// to multiple database
    /// </summary>
    public interface IMultiSessionUnitOfWork
    {
        /// <summary>
        /// Gets or sets the unit of works dictionary.
        /// </summary>
        /// <value>
        /// The unit of works dictionary.
        /// </value>
        IDictionary<string, IUnitOfWork> UnitOfWorks { get; set; }
    }
}