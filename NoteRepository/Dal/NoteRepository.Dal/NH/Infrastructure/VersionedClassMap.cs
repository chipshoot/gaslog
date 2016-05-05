using NoteRepository.Common.Utility.Dal;

namespace NoteRepository.Dal.NH.Infrastructure
{
    public abstract class VersionedClassMap<T> : EntityMap<T> where T : VersionedEntity
    {
        protected VersionedClassMap()
        {
            Version(x => x.Version).Column("ts").CustomSqlType("Rowversion").Generated.Always().UnsavedValue("null");
        }
    }
}