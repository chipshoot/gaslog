using NoteRepository.Common.Utility.Dal;
using System;

namespace NoteRepository.DomainEntity
{
    public class Note : VersionedEntity
    {
        public virtual string Subject { get; set; }

        public virtual string Content { get; set; }

        public virtual NoteCatalog Catalog { get; set; }

        public virtual NoteRender Render { get; set; }

        public virtual User Author { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public virtual DateTime LastModifiedDate { get; set; }

        public virtual string Description { get; set; }
    }
}