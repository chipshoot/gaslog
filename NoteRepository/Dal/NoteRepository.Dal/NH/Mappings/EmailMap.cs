using NoteRepository.Dal.NH.Infrastructure;
using NoteRepository.DomainEntity;

namespace NoteRepository.Dal.NH.Mappings
{
    public class EmailMap : EntityMap<Email>
    {
        public EmailMap()
        {
            Table("Emails");
            Map(x => x.Address).Column("Email");
            References(x => x.Owner);
            Map(x => x.IsPrimary);
            References(x => x.Catalog);
            Map(x => x.IsActivated);
            Map(x => x.Description);
        }
    }
}