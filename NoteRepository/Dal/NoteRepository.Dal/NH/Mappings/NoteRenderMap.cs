using NoteRepository.Dal.NH.Infrastructure;
using NoteRepository.DomainEntity;

namespace NoteRepository.Dal.NH.Mappings
{
    public class NoteRenderMap : EntityMap<NoteRender>
    {
        public NoteRenderMap()
        {
            Table("NoteRenders");
            Map(x => x.Name);
            Map(x => x.NameSpace);
            Map(x => x.Description);
        }
    }
}