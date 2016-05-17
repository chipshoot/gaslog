using NoteRepository.Common.Utility.Dal;

namespace NoteRepository.Core.DomainEntity
{
    public class Phone : Entity
    {
        public virtual int? AreaCode { get; set; }

        public virtual string Number { get; set; }

        public virtual int? Extension { get; set; }

        public virtual User Owner { get; set; }

        public virtual ContactInfoCatalog Catalog { get; set; }

        public virtual string Country { get; set; }

        public virtual bool IsActivated { get; set; }

        public virtual string Description { get; set; }
    }
}