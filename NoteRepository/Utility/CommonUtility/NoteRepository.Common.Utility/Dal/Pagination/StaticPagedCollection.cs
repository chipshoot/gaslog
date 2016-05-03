using System;
using System.Collections.Generic;

namespace NoteRepository.Common.Utility.Dal.Pagination
{
    /// <summary>
    /// Represents a subset of a collection of objects that can be individually accessed by index and
    /// containing meta data about the superset collection of objects this subset was created from.
    /// </summary>
    /// <remarks>
    /// Represents a subset of a collection of objects that can be individually accessed by index and
    /// containing meta data about the superset collection of objects this subset was created from.
    /// </remarks>
    /// <typeparam name="T">The type of object the collection should contain.</typeparam>
    /// <seealso cref="IPagedCollection{T}"/>
    /// <seealso cref="PagedCollectionBase{T}"/>
    /// <seealso cref="PagedCollection{T}"/>
    /// <seealso cref="List{T}"/>
    public class StaticPagedCollection<T> : PagedCollectionBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticPagedCollection{T}"/> class that contains
        /// the already divided subset and information about the size of the superset and the subset's position within it.
        /// </summary>
        /// <param name="subset">The single subset this collection should represent.</param>
        /// <param name="metaData">Supply the ".MetaData" property of an existing IPagedCollection instance to
        /// recreate it here (such as when creating a new instance of a PagedCollection after having used Auto mapper to
        /// convert its contents to a DTO.)</param>
        /// <exception cref="ArgumentOutOfRangeException">The specified index cannot be less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The specified page size cannot be less than one.</exception>
        public StaticPagedCollection(IEnumerable<T> subset, IPagedCollection metaData)
            : this(subset, metaData.PageNumber, metaData.PageSize, metaData.TotalItemCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticPagedCollection{T}"/> class that contains the
        /// already divided subset and information about the size of the superset and the subset's position within it.
        /// </summary>
        /// <param name="subset">The single subset this collection should represent.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <param name="totalItemCount">The size of the superset.</param>
        /// <exception cref="ArgumentOutOfRangeException">The specified index cannot be less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The specified page size cannot be less than one.</exception>
        public StaticPagedCollection(IEnumerable<T> subset, int pageNumber, int pageSize, int totalItemCount)
            : base(pageNumber, pageSize, totalItemCount)
        {
            var subSet = Subset as List<T>;
            if (subSet != null)
            {
                subSet.AddRange(subset);
            }
        }
    }
}