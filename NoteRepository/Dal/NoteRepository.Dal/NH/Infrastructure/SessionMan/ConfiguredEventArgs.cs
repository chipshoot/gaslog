using NHibernate.Cfg;
using System;

namespace NoteRepository.Dal.NH.Infrastructure.SessionMan
{
    /// <summary>
    /// Provides data for the <see cref="IConfigurationProvider.AfterConfigure"/> event.
    /// </summary>
    /// <seealso cref="ConfiguringEventArgs"/>
    public class ConfiguredEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfiguredEventArgs"/> class.
        /// </summary>
        /// <param name="configuration">An instance of <see cref="NHibernate.Cfg.Configuration"/> <b>(configured)</b>.</param>
        public ConfiguredEventArgs(Configuration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// The <b>configured</b> <see cref="NHibernate.Cfg.Configuration"/>
        /// </summary>
        public Configuration Configuration { get; private set; }
    }
}