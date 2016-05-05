using NoteRepository.Common.Utility.Dal;

namespace NoteRepository.DomainEntity
{
    public class ContactInfoCatalog : Entity
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
    }
}