using NoteRepository.Dal.NH.Infrastructure;
using NoteRepository.DomainEntity;

namespace NoteRepository.Dal.NH.Mappings
{
    public class NoteMap : VersionedClassMap<Note>
    {
        public NoteMap()
        {
            Table("Notes");
            Map(x => x.Subject);
            Map(x => x.Content);
            References(x => x.Catalog);
            References(x => x.Render);
            References(x => x.Author);
            Map(x => x.CreateDate);
            Map(x => x.LastModifiedDate);
            Map(x => x.Description);
        }
    }
}