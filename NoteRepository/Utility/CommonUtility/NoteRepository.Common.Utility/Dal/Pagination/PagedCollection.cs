﻿using NoteRepository.Common.Utility.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoteRepository.Common.Utility.Dal.Pagination
{
    /// <summary>
    /// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata about the superset collection of objects this subset was created from.
    /// </summary>
    /// <remarks>
    /// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata about the superset collection of objects this subset was created from.
    /// </remarks>
    /// <typeparam name="T">The type of object the collection should contain.</typeparam>
    /// <seealso cref="IPagedCollection{T}"/>
    /// <seealso cref="PagedCollectionBase{T}"/>
    /// <seealso cref="StaticPagedCollection{T}"/>
    /// <seealso cref="List{T}"/>
    [Serializable]
    public class PagedCollection<T> : PagedCollectionBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedCollection{T}"/> class that divides the supplied superset into subsets the size of the supplied pageSize. The instance then only containes the objects contained in the subset specified by index.
        /// </summary>
        /// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IQueryable{T}"/>, it will be treated as such.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <exception cref="ArgumentOutOfRangeException">The specified index cannot be less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The specified page size cannot be less than one.</exception>
        public PagedCollection(IQueryable<T> superset, int pageNumber, int pageSize)
        {
            Guard.Against<ArgumentOutOfRangeException>(pageNumber < 1, "PageNumber cannot be below 1.");
            Guard.Against<ArgumentOutOfRangeException>(pageSize < 1, "PageSize cannot be less than 1.");

            // set source to blank list if superset is null to prevent exceptions
            TotalItemCount = superset == null ? 0 : superset.Count();
            PageSize = pageSize;
            PageNumber = pageNumber;
            PageCount = TotalItemCount > 0
                        ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                        : 0;
            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < PageCount;
            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber >= PageCount;
            FirstItemOnPage = (PageNumber - 1) * PageSize + 1;
            var numberOfLastItemOnPage = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = numberOfLastItemOnPage > TotalItemCount
                            ? TotalItemCount
                            : numberOfLastItemOnPage;

            // add items to internal list
            var subSet = Subset as List<T>;
            if (subSet != null && superset != null && TotalItemCount > 0)
                subSet.AddRange(pageNumber == 1
                    ? superset.Skip(0).Take(pageSize).ToList()
                    : superset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()
                );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedCollection{T}"/> class that divides the supplied superset into subsets the size of the supplied pageSize. The instance then only containes the objects contained in the subset specified by index.
        /// </summary>
        /// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IQueryable{T}"/>, it will be treated as such.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <exception cref="ArgumentOutOfRangeException">The specified index cannot be less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The specified page size cannot be less than one.</exception>
        public PagedCollection(IEnumerable<T> superset, int pageNumber, int pageSize)
            : this(superset.AsQueryable(), pageNumber, pageSize)
        {
        }
    }
}