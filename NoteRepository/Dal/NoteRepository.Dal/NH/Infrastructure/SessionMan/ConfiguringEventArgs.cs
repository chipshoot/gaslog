using NHibernate.Cfg;
using System;

namespace NoteRepository.Dal.NH.Infrastructure.SessionMan
{
    /// <summary>
    /// Provides data for the <see cref="IConfigurationProvider.BeforeConfigure"/> event
    /// </summary>
    public class ConfiguringEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfiguringEventArgs"/> class.
        /// </summary>
        /// <param name="configuration">An instance of <see cref="NHibernate.Cfg.Configuration"/> not configured.</param>
        /// <remarks>
        /// At this point the method <see cref="NHibernate.Cfg.Configuration.Configure()"/>
        /// should not be called.
        /// </remarks>
        public ConfiguringEventArgs(Configuration configuration)
        {
            Configuration = configuration;
            Configured = false;
        }

        /// <summary>
        /// The not-configured <see cref="NHibernate.Cfg.Configuration"/>
        /// </summary>
        public Configuration Configuration { get; private set; }

        /// <summary>
        /// Set to <see langword="true"/> if your event handler are managing the whole NHibernate
        /// configuration process (it call <see cref="NHibernate.Cfg.Configuration.Configure()"/> or one of
        /// its overloads).
        /// </summary>
        public bool Configured { get; private set; }
    }
}