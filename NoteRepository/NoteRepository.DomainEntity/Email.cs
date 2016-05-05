using NoteRepository.Common.Utility.Dal;

namespace NoteRepository.DomainEntity
{
    public class Email : Entity
    {
        public virtual string Address { get; set; }

        public virtual User Owner { get; set; }

        public virtual ContactInfoCatalog Catalog { get; set; }

        public virtual bool IsPrimary { get; set; }

        public virtual bool IsActivated { get; set; }

        public virtual string Description { get; set; }
    }
}