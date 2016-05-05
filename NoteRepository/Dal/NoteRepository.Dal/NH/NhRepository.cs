using System.Linq;
using NoteRepository.Common.Utility.Dal;

namespace NoteRepository.Dal.NH
{
    /// <summary>
    /// repository how a list of domain entities and its process
    /// </summary>
    /// <typeparam name="T">the type of entity managed by repository</typeparam>
    public class NhRepository<T> : NhGenericRepository<T>, IRepository<T> where T : Entity
    {
        public NhRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Finds the entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="Entity"/>.</param>
        /// <returns><see cref="Entity"/> with id has been found from data source, otherwise null</returns>
        public T FindEntityById(int id)
        {
            return FindEntities().FirstOrDefault(e => e.Id == id);
        }
    }
}