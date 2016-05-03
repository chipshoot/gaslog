namespace NoteRepository.Common.Utility.Dal
{
    /// <summary>
    /// The <see cref="IRepository{TEntity}"/> interface defines a standard contract that repository
    /// components should implement, all <see cref="StringIdEntity"/> of the repository gets string
    /// as its unique id
    /// </summary>
    /// <typeparam name="T">The entity type we want to managed in the repository</typeparam>
    public interface IStringIdRepository<T> : IGenericRepository<T> where T : StringIdEntity
    {
        /// <summary>
        /// Finds the entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="Entity"/>.</param>
        /// <returns><see cref="Entity"/> with id has been found from data source, otherwise null</returns>
        T FindEntityById(string id);
    }
}