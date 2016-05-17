using NoteRepository.Common.Utility.Dal;

namespace NoteRepository.Core.DomainEntity
{
    public class NoteRender : Entity
    {
        public virtual string Name { get; set; }

        public virtual string NameSpace { get; set; }

        public virtual string Description { get; set; }
    }
}