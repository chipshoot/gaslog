namespace NoteRepository.Common.Utility.Dal
{
    /// <summary>
    /// The look up repository support searching data from multiple database source,
    /// to switch, just set the data source name
    /// </summary>
    public interface IMultiSourceLookupRepository : ILookupRepository
    {
        /// <summary>
        /// Gets or sets the name of the data source, which point to
        /// specific database
        /// </summary>
        /// <value>
        /// The name of the data source.
        /// </value>
         string DataSourceName { get; set; }
    }
}