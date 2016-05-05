﻿using NoteRepository.Common.Utility.Dal;

namespace NoteRepository.DomainEntity
{
    public class AutoMobile : Entity
    {
        public virtual string Name { get; set; }

        public virtual string SeriesNumber { get; set; }

        public virtual string Description { get; set; }
    }
}