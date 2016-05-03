using System;

namespace NoteRepository.Common.Utility.Dal
{
    /// <summary>
    /// The <see cref="IRepository{TEntity}"/> interface defines a standard contract that repository
    /// components should implement, all <see cref="GuidIdEntity"/> of repository has Guid as it unique id
    /// </summary>
    /// <typeparam name="T">The entity type we want to managed in the repository</typeparam>
    public interface IGuidRepository<T> : IGenericRepository<T> where T : GuidIdEntity
    {
        /// <summary>
        /// Finds the entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="Entity"/>.</param>
        /// <returns><see cref="Entity"/> with id has been found from data source, otherwise null</returns>
        T FindEntityById(Guid id);
    }
}