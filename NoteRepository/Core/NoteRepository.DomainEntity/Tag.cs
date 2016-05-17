using NoteRepository.Common.Utility.Dal;

namespace NoteRepository.Core.DomainEntity
{
    public class Tag : Entity
    {
        public virtual string Name { get; set; }

        public virtual bool IsActivated { get; set; }

        public virtual string Description { get; set; }
    }
}