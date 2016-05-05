using FluentNHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Engine;
using NoteRepository.Common.Utility.LogService;
using NoteRepository.Common.Utility.Validation;
using System;

namespace NoteRepository.Dal.NH.Infrastructure.SessionMan
{
    /// <summary>
    /// The provider is used for connecting database for NHibernate
    /// <example>
    ///
    /// var provider = new SessionFactoryProvider();
    ///
    /// </example>
    /// </summary>
    [Serializable]
    public class SessionFactoryProvider : ISessionFactoryProvider
    {
        #region private fields

        private static ILogService _logger;

        [NonSerialized]
        private IConfigurationProvider _configProvider;

        private bool _disposed;
        private ISessionFactory _factory;

        #endregion private fields

        #region constructors

        public SessionFactoryProvider(ILogService logger, IConfigurationProvider configProvider)
        {
            Guard.Against<ArgumentNullException>(logger == null, "logger");
            Guard.Against<ArgumentNullException>(configProvider == null, "configProvider");
            _logger = logger;
            _configProvider = configProvider;

            // ReSharper disable once PossibleNullReferenceException
            _configProvider.AfterConfigure += HookupMappings;
        }

        #endregion constructors

        #region public properties

        public PersistenceModel Model { get; set; }

        #endregion public properties

        #region implementation of interface ISessionFactoryProvider

        public event EventHandler<EventArgs> BeforeCloseSessionFactory;

        /// <summary>
        /// Gets the session factory by name.
        /// </summary>
        /// <returns>The session factory</returns>
        public ISessionFactory GetFactory()
        {
            Initialize();
            return _factory;
        }

        #endregion implementation of interface ISessionFactoryProvider

        #region implementation of interface IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion implementation of interface IDisposable

        #region private methods

        private void DoBeforeCloseSessionFactory()
        {
            BeforeCloseSessionFactory?.Invoke(this, new EventArgs());
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_factory != null)
                    {
                        DoBeforeCloseSessionFactory();
                        _factory.Close();
                    }

                    _factory = null;
                }

                _disposed = true;
            }
        }

        private void Initialize()
        {
            if (_factory != null)
            {
                return;
            }

            _logger.Debug("Initialize new session factories reading the configuration.");

            var cfg = _configProvider.Configure();
            var sf = (ISessionFactoryImplementor)cfg.BuildSessionFactory();
            _factory = sf;

            // after built the SessionFactory, the configuration object is not needed and can be removed
            _configProvider = null;
        }

        private void HookupMappings(object sender, ConfiguredEventArgs args)
        {
            var cfg = args.Configuration;

            cfg.SessionFactory().GenerateStatistics();

            Model = new DefaultModel();
            Model.Configure(cfg);
        }

        #endregion private methods
    }
}