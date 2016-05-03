using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace NoteRepository.Common.Utility.Dal.Queries
{
    public interface IQueryConstraints<T> where T : class
    {
        /// <summary>
        /// Gets the kind of sort order
        /// </summary>
        ListSortDirection SortOrder { get; }

        /// <summary>
        /// Gets property name for the property to sort by.
        /// </summary>
        string SortPropertyName { get; }

        /// <summary>
        /// Gets or sets the query expression which will applied to
        /// target domain entities.
        /// </summary>
        /// <value>The query expression which will be applied.</value>
        Expression<Func<T, bool>> WithQuery{ get; set; }

        /// <summary>
        /// Sort ascending by a property
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Current instance</returns>
        IQueryConstraints<T> SortBy(string propertyName);

        /// <summary>
        /// Sort descending by a property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Current instance</returns>
        IQueryConstraints<T> SortByDescending(string propertyName);

        /// <summary>
        /// Property to sort by (ascending)
        /// </summary>
        /// <param name="property">The property.</param>
        IQueryConstraints<T> SortBy(Expression<Func<T, object>> property);

        /// <summary>
        /// Property to sort by (descending)
        /// </summary>
        /// <param name="property">The property</param>
        IQueryConstraints<T> SortByDescending(Expression<Func<T, object>> property);
    }
}