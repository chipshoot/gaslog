namespace NoteRepository.Common.Utility.Dal
{
    public class VersionedEntity : Entity
    {
        public virtual byte[] Version { get; set; }
    }
}