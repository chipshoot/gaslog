using System.ComponentModel;
using NoteRepository.Common.Utility.Validation;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NoteRepository.Common.Utility.Dal.Queries
{
    public static class Extensions
    {
        /// <summary>
        /// Gets the member info.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        /// <remarks>Used to get property information</remarks>
        public static MemberExpression GetMemberInfo(this Expression method)
        {
            var lambda = method as LambdaExpression;
            Guard.Against<ArgumentNullException>(lambda == null, "method");

            MemberExpression memberExpression = null;
            // ReSharper disable once PossibleNullReferenceException
            switch (lambda.Body.NodeType)
            {
                case ExpressionType.Convert:
                    memberExpression = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
                    break;

                case ExpressionType.MemberAccess:
                    memberExpression = lambda.Body as MemberExpression;
                    break;
            }

            if (memberExpression == null)
            {
                throw new ArgumentException("method");
            }

            return memberExpression;
        }

        /// <summary>
        /// Apply the query information to a LINQ statement
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="instance">constraints instance</param>
        /// <param name="query">LINQ queryable</param>
        /// <returns>Modified query</returns>
        public static IQueryable<T> ApplyTo<T>(this IQueryConstraints<T> instance, IQueryable<T> query) where T : class
        {
            Guard.Against<ArgumentNullException>(instance == null, "instance");
            Guard.Against<ArgumentNullException>(query == null, "query");

            // ReSharper disable once PossibleNullReferenceException
            if (instance.WithQuery != null)
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                query = query.Where(instance.WithQuery);
            }

            if (!string.IsNullOrEmpty(instance.SortPropertyName))
            {
                query = instance.SortOrder == ListSortDirection.Ascending
                            ? query.OrderBy(instance.SortPropertyName)
                            : query.OrderByDescending(instance.SortPropertyName);
            }

            return query;
        }

        /// <summary>
        /// Apply ordering to a LINQ query
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="source">Linq query</param>
        /// <param name="propertyName">Property to sort by</param>
        /// <returns>Ordered query</returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (propertyName == null) throw new ArgumentNullException("propertyName");

            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var resultExp = Expression.Call(typeof(Queryable), "OrderBy", new[] { type, property.PropertyType },
                                            source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        /// <summary>
        /// Apply ordering to a LINQ query
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="source">Linq query</param>
        /// <param name="propertyName">Property to sort by</param>
        /// <returns>Ordered query</returns>
        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            Guard.Against<ArgumentNullException>(source == null, "source");
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(propertyName), "propertyName");

            var type = typeof(T);

            // ReSharper disable once AssignNullToNotNullAttribute
            var property = type.GetProperty(propertyName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            // ReSharper disable once PossibleNullReferenceException
            var resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}