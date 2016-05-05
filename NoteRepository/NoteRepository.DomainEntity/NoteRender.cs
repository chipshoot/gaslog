using NoteRepository.Common.Utility.Dal;

namespace NoteRepository.DomainEntity
{
    public class NoteRender : Entity
    {
        public virtual string Name { get; set; }

        public virtual string NameSpace { get; set; }

        public virtual string Description { get; set; }
    }
}