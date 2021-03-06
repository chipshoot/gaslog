﻿using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace NoteRepository.Common.Utility.Dal
{
    /// <summary>
    /// The <see cref="ILookupRepository"/> interface defines a standard contract that
    /// deal with lookup data.
    /// </summary>
    public interface ILookupRepository
    {
        /// <summary>
        /// Grab all item in a ItemRepository
        /// </summary>
        /// <typeparam name="T">The item type we want to find</typeparam>
        /// <returns>The List of items found in parent</returns>
        IQueryable<T> FindEntities<T>() where T : class;

        /// <summary>
        /// Gets the list of entities that match criteria.
        /// </summary>
        /// <param name="query">The query to search the data source.</param>
        /// <returns>
        /// The list of entity that match the criteria
        /// </returns>
        IQueryable<T> FindEntities<T>(Expression<Func<T, bool>> query) where T : class;

        /// <summary>
        /// Quicks the search the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="targetList">The target list.</param>
        /// <returns></returns>
        IList QuickSearch<T>(string query, IList targetList);
    }
}