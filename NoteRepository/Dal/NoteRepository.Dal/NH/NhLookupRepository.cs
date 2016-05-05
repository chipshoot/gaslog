using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using NoteRepository.Common.Utility.Dal;
using NoteRepository.Common.Utility.Validation;
using NHibernate;
using NHibernate.Linq;

namespace NoteRepository.Dal.NH
{
    /// <summary>
    /// The <see cref="ILookupRepository"/> which implement under NHibernate section
    /// </summary>
    public class NhLookupRepository : ILookupRepository
    {
        #region private fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion private fields

        #region constructors

        public NhLookupRepository(IUnitOfWork unitOfWork)
        {
            Guard.Against<ArgumentNullException>(unitOfWork == null, "unitOfWork");
            _unitOfWork = unitOfWork;
        }

        #endregion constructors

        #region Implementation of ILookupRepository

        /// <summary>
        /// Grab all item in a ItemRepository
        /// </summary>
        /// <typeparam name="T">The item type we want to find</typeparam>
        /// <returns>The List of items found in parent</returns>
        public IQueryable<T> FindEntities<T>() where T : class
        {
            return GetSession().Query<T>();
        }

        /// <summary>
        /// Gets the list of entities that match criteria.
        /// </summary>
        /// <param name="query">The query to search the data source.</param>
        /// <returns>
        /// The list of entity that match the criteria
        /// </returns>
        public IQueryable<T> FindEntities<T>(Expression<Func<T, bool>> query) where T : class
        {
            var session = GetQuickSession();
            return session.Query<T>().Where(query);
        }

        public IList QuickSearch<T>(string query, IList targetList)
        {
            Guard.Against<ArgumentNullException>(targetList == null, "targetList");
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(query), "query");

            GetQuickSession().CreateQuery(query).List(targetList);

            return targetList;
        }

        #endregion Implementation of ILookupRepository

        #region public methods

        public IQuery CreateQuery(string query)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(query), "query");
            return GetSession().CreateQuery(query);
        }

        #endregion public methods

        #region private method

        private ISession GetSession()
        {
            return ((NhUnitOfWork)_unitOfWork).Session;
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <returns>ISession.</returns>
        private IStatelessSession GetQuickSession()
        {
            return ((NhUnitOfWork)_unitOfWork).QuickSession;
        }

        #endregion private method
    }
}