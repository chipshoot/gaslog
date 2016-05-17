using NoteRepository.Dal.NH.Infrastructure;
using NoteRepository.Core.DomainEntity;

namespace NoteRepository.Dal.NH.Mappings
{
    public class NoteCatalogMap : EntityMap<NoteCatalog>
    {
        public NoteCatalogMap()
        {
            Table("NoteCatalogues");
            Map(x => x.Name);
            Map(x => x.Description);
        }
    }
}