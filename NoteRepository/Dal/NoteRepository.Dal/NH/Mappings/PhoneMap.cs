using NoteRepository.Dal.NH.Infrastructure;
using NoteRepository.DomainEntity;

namespace NoteRepository.Dal.NH.Mappings
{
    public class PhoneMap : EntityMap<Phone>
    {
        public PhoneMap()
        {
            Table("Phones");
            Map(x => x.Number).Column("Phone");
            References(x => x.Owner);
            Map(x => x.Country);
            References(x => x.Catalog);
            Map(x => x.IsActivated);
            Map(x => x.Description);
        }
    }
}