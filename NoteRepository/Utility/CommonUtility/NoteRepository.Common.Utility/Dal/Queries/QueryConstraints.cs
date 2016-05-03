using System.ComponentModel;
using System.Globalization;
using NoteRepository.Common.Utility.Validation;
using System;
using System.Linq.Expressions;

namespace NoteRepository.Common.Utility.Dal.Queries
{
    public class QueryConstraints<T> : IQueryConstraints<T> where T : class
    {
        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryConstraints{T}"/> class.
        /// </summary>
        /// <remarks>Will per default return the first 50 items</remarks>
        public QueryConstraints()
        {
            ModelType = typeof(T);
        }

        #endregion constructor

        #region protected properties

        protected Type ModelType { get; set; }

        #endregion protected properties

        #region implementation of interface IQueryConstraints

        /// <summary>
        /// Gets the kind of sort order
        /// </summary>
        public ListSortDirection SortOrder { get; private set; }

        /// <summary>
        /// Gets property name for the property to sort by.
        /// </summary>
        public string SortPropertyName { get; private set; }

        /// <summary>
        /// Gets or sets the query expression which will applied to
        /// target domain entities.
        /// </summary>
        /// <value>The query expression which will be applied.</value>
        public Expression<Func<T, bool>> WithQuery { get; set; }

        /// <summary>
        /// Sort ascending by a property
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Current instance</returns>
        public IQueryConstraints<T> SortBy(string propertyName)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(propertyName), "propertyName");

            ValidatePropertyName(propertyName);

            SortOrder = ListSortDirection.Ascending;
            SortPropertyName = propertyName;
            return this;
        }

        /// <summary>
        /// Sort descending by a property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Current instance</returns>
        public IQueryConstraints<T> SortByDescending(string propertyName)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(propertyName), "propertyName");
            ValidatePropertyName(propertyName);

            SortOrder = ListSortDirection.Descending;
            SortPropertyName = propertyName;
            return this;
        }

        /// <summary>
        /// Property to sort by (ascending)
        /// </summary>
        /// <param name="property">The property.</param>
        public IQueryConstraints<T> SortBy(Expression<Func<T, object>> property)
        {
            Guard.Against<ArgumentNullException>(property == null, "property");
            var expression = property.GetMemberInfo();
            var name = expression.Member.Name;
            SortBy(name);
            return this;
        }

        /// <summary>
        /// Property to sort by (descending)
        /// </summary>
        /// <param name="property">The property</param>
        public IQueryConstraints<T> SortByDescending(Expression<Func<T, object>> property)
        {
            Guard.Against<ArgumentNullException>(property == null, "property");

            var expression = property.GetMemberInfo();
            var name = expression.Member.Name;
            SortByDescending(name);
            return this;
        }

        #endregion implementation of interface IQueryConstraints

        #region private methods

        /// <summary>
        /// Make sure that the property exists in the model.
        /// </summary>
        /// <param name="name">The name.</param>
        protected virtual void ValidatePropertyName(string name)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(name), "name");

            // ReSharper disable once AssignNullToNotNullAttribute
            if (ModelType.GetProperty(name) == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "'{0}' is not a public property of '{1}'.", name, ModelType.FullName));
            }
        }

        #endregion private methods
    }
}