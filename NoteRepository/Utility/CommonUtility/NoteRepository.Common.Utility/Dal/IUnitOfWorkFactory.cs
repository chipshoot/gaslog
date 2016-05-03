namespace NoteRepository.Common.Utility.Dal
{
    /// <summary>
    /// This is the interface between domain entity to database.
    /// Client can query the entity through this interface and
    /// get UnitOfWork for change the data in database
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        #region methods

        /// <summary>
        /// Gets the current <see cref="IMultiSessionUnitOfWork"/> which
        /// connect to multi database e.g. JDE and HPC
        /// </summary>
        /// <returns>The unique lookup unit of work for current domain entity</returns>
        IMultiSessionUnitOfWork CurrentUnitOfWork { get; }

        /// <summary>
        /// Gets the current <see cref="IUnitOfWork"/> which against HPC database.
        /// </summary>
        /// <value>
        /// The current HPC connected <see cref="IUnitOfWork"/>.
        /// </value>
        IUnitOfWork CurrentHpcUnitOfWork { get; }

        /// <summary>
        /// Gets the current <see cref="IUnitOfWork" /> which against JDE database.
        /// <remarks>
        /// Currently system will not update JDE Database directly via database connection
        /// so we can set the session to read-only to resolve the performance issue
        /// </remarks>
        /// </summary>
        /// <value>The current JDE connected <see cref="IUnitOfWork" />.</value>
        IUnitOfWork CurrentJdeUnitOfWork { get; }

        /// <summary>
        /// Binds the unit of work in certain context for MVC and WCF web services.
        /// </summary>
        void BindUnitOfWork();

        /// <summary>
        /// Closes the unit of work.
        /// </summary>
        void CloseUnitOfWork();

        #endregion methods
    }
}