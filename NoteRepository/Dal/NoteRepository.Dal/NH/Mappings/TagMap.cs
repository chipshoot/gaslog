using NoteRepository.Dal.NH.Infrastructure;
using NoteRepository.DomainEntity;

namespace NoteRepository.Dal.NH.Mappings
{
    public class TagMap : EntityMap<Tag>
    {
        public TagMap()
        {
            Table("Tags");
            Map(x => x.Name);
            Map(x => x.IsActivated);
            Map(x => x.Description);
        }
    }
}