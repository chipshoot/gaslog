using NHibernate.Cfg;
using NoteRepository.Common.Utility.LogService;
using NoteRepository.Common.Utility.Validation;
using System;

namespace NoteRepository.Dal.NH.Infrastructure.SessionMan
{
    /// <summary>
    /// The base class of all configuration provider
    /// </summary>
    public abstract class ConfigurationProviderBase : IConfigurationProvider
    {
        #region constructor

        protected ConfigurationProviderBase(ILogService logger)
        {
            Guard.Against<ArgumentNullException>(logger == null, "logger");
            Logger = logger;
        }

        #endregion constructor

        #region Implementation of IConfigurationProvider

        public abstract Configuration Configure();

        public event EventHandler<ConfiguringEventArgs> BeforeConfigure;

        public event EventHandler<ConfiguredEventArgs> AfterConfigure;

        #endregion Implementation of IConfigurationProvider

        #region protected properties

        protected ILogService Logger { get; private set; }

        #endregion protected properties

        #region protected methods

        protected void DoAfterConfigure(Configuration cfg)
        {
            AfterConfigure?.Invoke(this, new ConfiguredEventArgs(cfg));
        }

        protected void DoBeforeConfigure(Configuration cfg, out bool configured)
        {
            configured = false;
            if (BeforeConfigure == null)
            {
                return;
            }

            var args = new ConfiguringEventArgs(cfg);
            BeforeConfigure(this, args);
            configured = args.Configured;
        }

        #endregion protected methods
    }
}