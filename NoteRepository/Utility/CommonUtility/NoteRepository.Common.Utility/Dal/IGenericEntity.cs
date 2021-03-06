﻿using System;

namespace NoteRepository.Common.Utility.Dal
{
    /// <summary>
    /// The base interface of all entity in domain
    /// </summary>
    /// <typeparam name="TIdentity">The type of the identity.</typeparam>
    public interface IGenericEntity<TIdentity> : IEquatable<IGenericEntity<TIdentity>>
    {
        /// <summary>
        /// Gets the id of the domain entity.
        /// </summary>
        TIdentity Id { get; }
    }
}