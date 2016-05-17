using NoteRepository.Dal.NH.Infrastructure;
using NoteRepository.Core.DomainEntity;

namespace NoteRepository.Dal.NH.Mappings
{
    public class EmailMap : EntityMap<Email>
    {
        public EmailMap()
        {
            Table("Emails");
            Map(x => x.Address).Column("Email");
            References(x => x.Owner).Column("Owner");
            Map(x => x.IsPrimary);
            References(x => x.Catalog).Column("Catalog");
            Map(x => x.IsActivated);
            Map(x => x.Description);
        }
    }
}