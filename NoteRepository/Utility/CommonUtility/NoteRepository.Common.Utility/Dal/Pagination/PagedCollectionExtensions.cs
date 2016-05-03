using NoteRepository.Common.Utility.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoteRepository.Common.Utility.Dal.Pagination
{
    /// <summary>
    /// Container for extension methods designed to simplify the creation of instances of <see cref="PagedCollection{T}"/>.
    /// </summary>
    public static class PagedCollectionExtensions
    {
        /// <summary>
        /// Creates a subset of this collection of objects that can be individually accessed by index and
        /// containing meta data about the collection of objects the subset was created from.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>
        /// <param name="superset">The collection of objects to be divided into subsets. If the collection
        /// implements <see cref="IQueryable{T}"/>, it will be treated as such.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by
        /// this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <returns>A subset of this collection of objects that can be individually accessed by index and
        /// containing meta data about the collection of objects the subset was created from.</returns>
        /// <seealso cref="PagedCollection{T}"/>
        public static IPagedCollection<T> ToPagedList<T>(this IEnumerable<T> superset, int pageNumber, int pageSize)
        {
            return new PagedCollection<T>(superset, pageNumber, pageSize);
        }

        /// <summary>
        /// Creates a subset of this collection of objects that can be individually accessed by index and containing
        /// meta data about the collection of objects the subset was created from.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>
        /// <param name="superset">The collection of objects to be divided into subsets. If the collection implements
        /// <see cref="IQueryable{T}"/>, it will be treated as such.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <returns>A subset of this collection of objects that can be individually accessed by index and containing
        /// meta data about the collection of objects the subset was created from.</returns>
        /// <seealso cref="PagedCollection{T}"/>
        public static IPagedCollection<T> ToPagedList<T>(this IQueryable<T> superset, int pageNumber, int pageSize)
        {
            return new PagedCollection<T>(superset, pageNumber, pageSize);
        }

        /// <summary>
        /// Splits a collection of objects into n pages with an (for example, if I have a list of 45 shoes and say
        /// 'shoes.Split(5)' I will now have 4 pages of 10 shoes and 1 page of 5 shoes.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>
        /// <param name="superset">The collection of objects to be divided into subsets.</param>
        /// <param name="numberOfPages">The number of pages this collection should be split into.</param>
        /// <returns>A subset of this collection of objects, split into n pages.</returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> superset, int numberOfPages)
        {
            return superset
                .Select((item, index) => new { index, item })
                .GroupBy(x => x.index % numberOfPages)
                .Select(x => x.Select(y => y.item));
        }

        /// <summary>
        /// Splits a collection of objects into an unknown number of pages with n items per page (for example,
        /// if I have a list of 45 shoes and say 'shoes.Partition(10)' I will now have 4 pages of 10 shoes and
        /// 1 page of 5 shoes.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>
        /// <param name="superset">The collection of objects to be divided into subsets.</param>
        /// <param name="pageSize">The maximum number of items each page may contain.</param>
        /// <returns>A subset of this collection of objects, split into pages of maximum size n.</returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> superset, int pageSize)
        {
            var enumerable = superset as IList<T> ?? superset.ToList();
            if (enumerable.Count() < pageSize)
            {
                yield return enumerable;
            }
            else
            {
                var numberOfPages = Math.Ceiling(enumerable.Count() / (double)pageSize);
                for (var i = 0; i < numberOfPages; i++)
                    yield return enumerable.Skip(pageSize * i).Take(pageSize);
            }
        }

        /// <summary>
        /// Converts one <see cref="IPagedCollectionCollection{T}"/> to other <see cref="IPagedCollectionCollection{T}"/> with
        /// specified converter. The method can be used to convert Domain Entity to Model class for MVVM
        /// or MVC application
        /// </summary>
        /// <typeparam name="TSrc">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="sourceSet">The source paged list.</param>
        /// <param name="converter">The instance of <see cref="IConverter{TSrc, TTarget}"/> who can get target object from
        /// source object.</param>
        /// <returns>IPagedCollection{TTarget} with new type items.</returns>
        public static IPagedCollection<TTarget> Convert<TSrc, TTarget>(this IPagedCollection<TSrc> sourceSet, IConverter<TSrc, TTarget> converter)
        {
            Guard.Against<ArgumentNullException>(converter == null, "converter");
            if (sourceSet == null)
            {
                return null;
            }

            // ReSharper disable once PossibleNullReferenceException
            var targetLst = sourceSet.Select(converter.Create).ToList();
            var ret = new StaticPagedCollection<TTarget>(targetLst, sourceSet);
            return ret;
        }
    }
}