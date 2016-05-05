using NoteRepository.Dal.NH.Infrastructure;
using NoteRepository.DomainEntity;

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