using NoteRepository.Dal.NH.Infrastructure;
using NoteRepository.Core.DomainEntity;

namespace NoteRepository.Dal.NH.Mappings
{
    public class PhoneMap : EntityMap<Phone>
    {
        public PhoneMap()
        {
            Table("Phones");
            Map(x => x.AreaCode);
            Map(x => x.Number).Column("Phone");
            Map(x => x.Extension);
            References(x => x.Owner).Column("Owner");
            Map(x => x.Country);
            References(x => x.Catalog).Column("Catalog");
            Map(x => x.IsActivated);
            Map(x => x.Description);
        }
    }
}