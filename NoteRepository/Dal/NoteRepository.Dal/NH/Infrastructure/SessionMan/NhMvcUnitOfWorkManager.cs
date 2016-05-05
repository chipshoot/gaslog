using FluentNHibernate;
using Hadrian.Common.Utility.Dal;
using Hadrian.Common.Utility.Dal.Exceptions;
using Hadrian.Common.Utility.LogService;
using Hadrian.ProjectCenter.Dal.JdeMap;
using Hadrian.ProjectCenter.DomainEntity.HadrianApp;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using System.Collections.Generic;

namespace Hadrian.ProjectCenter.Dal.NH.Infrastructure.SessionMan
{
    public class NhMvcUnitOfWorkManager
    {
        #region private fields

        private static ISessionFactoryProvider _sessionFactoryProvider;

        private static readonly ILogService Logger;

        #endregion private fields

        #region constructor

        static NhMvcUnitOfWorkManager()
        {
            Logger = new DefaultLogger();
            SetupSessionFactory();
        }

        #endregion constructor

        #region public properties

        public static PersistenceModel Model { get; set; }

        public static IDictionary<string, string> Properties { get; set; }

        public static IUnitOfWork CurrentHpcUnitOfWork => CurrentUnitOfWork.UnitOfWorks[ApplicationSetting.HpcDbSessionName];

        public static IUnitOfWork CurrentJdeUnitOfWork => CurrentUnitOfWork.UnitOfWorks[ApplicationSetting.JdeDbSessionName];

        public static IMultiSessionUnitOfWork CurrentUnitOfWork => GetUnitOfWork();

        #endregion public properties

        #region public static methods

        /// <summary>
        /// Binds the session without return any session object.
        /// </summary>
        public static void BindUnitOfWork()
        {
            var factory = _sessionFactoryProvider.GetFactory(ApplicationSetting.HpcDbSessionName);
            if (CurrentSessionContext.HasBind(factory))
            {
                return;
            }

            var session = factory.OpenSession();
            CurrentSessionContext.Bind(session);

            var jdeFactory = _sessionFactoryProvider.GetFactory(ApplicationSetting.HpcDbSessionName);
            if (CurrentSessionContext.HasBind(jdeFactory))
            {
                return;
            }

            var jdeSession = jdeFactory.OpenSession();
            CurrentSessionContext.Bind(jdeSession);
        }

        public static void CloseUnitOfWork()
        {
            CloseUnitOfWork(ApplicationSetting.HpcDbSessionName);

            CloseUnitOfWork(ApplicationSetting.JdeDbSessionName);

        }

        #endregion public static methods

        #region private static methods

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <returns>
        /// The unit of work which connecting to Hpc and Jde database
        /// </returns>
        /// <exception cref="UnitOfWorkException"></exception>
        private static IMultiSessionUnitOfWork GetUnitOfWork()
        {
            var uows = new Dictionary<string, IUnitOfWork>();

            // setup session factory which connect to HPC database
            var factory = _sessionFactoryProvider.GetFactory(ApplicationSetting.HpcDbSessionName);
            if (factory == null)
            {
                var errorMsg = $"Cannot find session factory for {ApplicationSetting.HpcDbSessionName}";
                throw new UnitOfWorkException(errorMsg);
            }

            ISession hpcSession;
            if (CurrentSessionContext.HasBind(factory))
            {
                hpcSession = factory.GetCurrentSession();
            }
            else
            {
                hpcSession = factory.OpenSession();
            }

            var hpcQuickSession = factory.OpenStatelessSession();
            var hpcUow = new NhUnitOfWork(hpcSession, hpcQuickSession);
            
            uows.Add(ApplicationSetting.HpcDbSessionName, hpcUow);

            // setup session factory which connect to JDE database
            var jdeFactory = _sessionFactoryProvider.GetFactory(ApplicationSetting.JdeDbSessionName);
            if (jdeFactory == null)
            {
                var errorMsg = $"Cannot find session factory for {ApplicationSetting.JdeDbSessionName}";
                throw new UnitOfWorkException(errorMsg);
            }

            var jdeSession = CurrentSessionContext.HasBind(factory) ? jdeFactory.GetCurrentSession() : jdeFactory.OpenSession();
            var jdeQuickSession = jdeFactory.OpenStatelessSession();
            var jdeUow = new NhUnitOfWork(jdeSession, jdeQuickSession);
            uows.Add(ApplicationSetting.JdeDbSessionName, jdeUow);

            var result = new NhMultiSessionUnitOfWork { UnitOfWorks = uows };
            return result;
        }

        /// <summary>
        /// Setups the session factory based on configuration.
        /// </summary>
        private static void SetupSessionFactory()
        {
            if (_sessionFactoryProvider != null)
            {
                return;
            }

            Logger.DebugFormat("Begin setup session factory...");

            var configProvider = new MultiSessionFactoryConfigurationProvider(Logger);
            configProvider.AfterConfigure += HookupMappings;
            _sessionFactoryProvider = new MultiSessionFactoryProvider(Logger, configProvider);
        }

        private static void HookupMappings(object sender, ConfiguredEventArgs args)
        {
            var name = args.ConfigurationName;
            var cfg = args.Configuration;

            cfg.SessionFactory().GenerateStatistics();

            // add mapping here
            switch (name)
            {
                case ApplicationSetting.HpcDbSessionName:

                    Model = new DefaultModel();
                    break;

                case ApplicationSetting.JdeDbSessionName:
                    Model = new JdeModel();
                    break;
            }

            Model.Configure(cfg);
        }

        private static void CloseUnitOfWork(string uowName)
        {
            if (string.IsNullOrEmpty(uowName))
            {
                return;
            }

            var factory = _sessionFactoryProvider.GetFactory(uowName);
            if (factory == null)
            {
                return;
            }

            if (!CurrentSessionContext.HasBind(factory))
            {
                return;
            }


            var uow = CurrentUnitOfWork?.UnitOfWorks[uowName];

            uow?.Flush();
            uow?.Dispose();

            CurrentSessionContext.Unbind(factory);
        }

        #endregion private static methods
    }
}