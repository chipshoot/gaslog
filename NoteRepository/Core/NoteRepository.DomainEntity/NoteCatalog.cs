using NoteRepository.Common.Utility.Dal;

namespace NoteRepository.Core.DomainEntity
{
    public class NoteCatalog : Entity
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
    }
}