using FluentNHibernate;
using NoteRepository.Dal.NH.Mappings;

namespace NoteRepository.Dal.NH.Infrastructure
{
    public class DefaultModel : PersistenceModel
    {
        public DefaultModel()
        {
            AddMappingsFromAssembly(typeof(UserMap).Assembly);
        }
    }
}