using NoteRepository.Dal.NH.Infrastructure;
using NoteRepository.Core.DomainEntity;

namespace NoteRepository.Dal.NH.Mappings
{
    public class ContactInfoCatalogMap : EntityMap<ContactInfoCatalog>
    {
        public ContactInfoCatalogMap()
        {
            Table("ContactInfoCatalogues");
            Map(x => x.Name);
            Map(x => x.Description);
        }
    }
}