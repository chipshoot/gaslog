using NoteRepository.Common.Utility.Dal;
using NoteRepository.Common.Utility.Validation;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NoteRepository.Dal.NH
{
    public class NhGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        #region constructors

        public NhGenericRepository(IUnitOfWork unitOfWork)
        {
            Guard.Against<ArgumentNullException>(unitOfWork == null, "unitOfWork");
            _unitOfWork = unitOfWork;
        }

        #endregion constructors

        #region Implementation of IGenericRepository<T>

        /// <summary>
        /// Grab all item in a ItemRepository
        /// </summary>
        /// <returns>The List of items found in parent</returns>
        public IQueryable<T> FindEntities()
        {
            return GetSession().Query<T>();
        }

        /// <summary>
        /// Gets the list of entities that match criteria.
        /// </summary>
        /// <param name="query">The query to search the data source.</param>
        /// <returns>The list of entity that match the criteria</returns>
        public IQueryable<T> FindEntities(Expression<Func<T, bool>> query)
        {
            return FindEntities().Where(query);
        }

        /// <summary>
        /// Adds the entity to data source.
        /// </summary>
        /// <param name="entity">the entity which will be added</param>
        /// <returns>The new added entity with id</returns>
        public T Add(T entity)
        {
            _unitOfWork.Add(entity);

            return entity;
        }

        /// <summary>
        /// Updates the specified entity of data source.
        /// </summary>
        /// <param name="entity">The entity which will be updated.</param>
        /// <returns>The new added entity with id</returns>
        public T Update(T entity)
        {
            _unitOfWork.Update(entity);

            return entity;
        }

        /// <summary>
        /// Deletes the specified entity from data source.
        /// </summary>
        /// <param name="entity">The entity which will be removed.</param>
        public void Delete(T entity)
        {
            _unitOfWork.Delete(entity);
        }

        /// <summary>
        /// Clear entity from data cache and let system retrieve data again from data source next time from <see cref="IGenericRepository{T}.FindEntities()"/>.
        /// Client need this after data source get changed
        /// </summary>
        /// <param name="entity">The entity need to be refreshed</param>
        public void Refresh(ref T entity)
        {
            var session = ((NhUnitOfWork)_unitOfWork).Session;
            session.Evict(entity);
        }

        /// <summary>
        /// Flushes the cached data to database.
        /// </summary>
        public void Flush()
        {
            _unitOfWork.Flush();
        }

        #endregion Implementation of IGenericRepository<T>

        #region private methods

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <returns>ISession.</returns>
        private ISession GetSession()
        {
            return ((NhUnitOfWork)_unitOfWork).Session;
        }

        #endregion private methods
    }
}