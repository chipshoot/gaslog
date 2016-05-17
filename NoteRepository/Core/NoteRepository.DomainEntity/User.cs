using NoteRepository.Common.Utility.Dal;
using System;

namespace NoteRepository.Core.DomainEntity
{
    public class User : Entity
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string AccountName { get; set; }

        public virtual DateTime BirthDay { get; set; }

        public virtual string Password { get; set; }

        public virtual string Salt { get; set; }

        public virtual bool IsActivated { get; set; }

        public virtual string Description { get; set; }
    }
}