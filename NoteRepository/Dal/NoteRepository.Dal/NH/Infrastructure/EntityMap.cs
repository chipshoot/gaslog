using FluentNHibernate.Mapping;
using NoteRepository.Common.Utility.Dal;

namespace NoteRepository.Dal.NH.Infrastructure
{
    public abstract class EntityMap<T> : ClassMap<T> where T : Entity
    {
        protected EntityMap()
        {
            Id(x => x.Id);
        }
    }
}