using NoteRepository.Common.Utility.LogService;
using System;
using System.Reflection;
using Configuration = NHibernate.Cfg.Configuration;

namespace NoteRepository.Dal.NH.Infrastructure.SessionMan
{
    /// <summary>
    /// The class is for getting configuration information for single database connection
    /// </summary>
    public class ConfigurationProvider : ConfigurationProviderBase
    {
        #region constructor

        public ConfigurationProvider(ILogService logger) : base(logger)
        {
        }

        #endregion constructor

        public override Configuration Configure()
        {
            Configuration config = null;
            Logger.Debug("Begin get configuration setting");
            try
            {
                Logger.DebugFormat("Setting config");

                config = new Configuration();

                bool configed;
                DoBeforeConfigure(config, out configed);
                DoAfterConfigure(config);
            }
            catch (Exception ex)
            {
                Logger.Error("something wrong", ex);

                // check to see if we missing some library in builds
                if (ex is ReflectionTypeLoadException)
                {
                    var typeLoadException = ex as ReflectionTypeLoadException;
                    var loaderExceptions = typeLoadException.LoaderExceptions;
                    foreach (var exp in loaderExceptions)
                    {
                        Logger.Error("loader exception", exp);
                    }
                }

                throw;
            }

            Logger.Debug("End get configuration setting");
            return config;
        }
    }
}