using FluentNHibernate;
using Hadrian.Common.Utility.Dal;
using Hadrian.Common.Utility.LogService;
using Hadrian.ProjectCenter.Dal.JdeMap;
using Hadrian.ProjectCenter.Dal.NH;
using Hadrian.ProjectCenter.Dal.NH.Infrastructure;
using Hadrian.ProjectCenter.Dal.NH.Infrastructure.SessionMan;
using Hadrian.ProjectCenter.DomainEntity.HadrianApp;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;

namespace NoteRepository.Common.TestHelper
{
    /// <summary>
    /// The class is used to setup testing environment for NHibernate to connect
    /// database, also it setup the all property need to be inject
    /// </summary>
    public class TestFixtureBase
    {
        #region private fields

        private static ISessionFactory _hpcSessionFactory;

        private static ISessionFactory _jdeSessionFactory;

        private static IUnitOfWork _hpcUnitOfWork;

        private static IUnitOfWork _jdeUnitOfWork;

        private static IMultiSessionUnitOfWork _multiUnitOfWork;

        private static ISessionFactoryProvider _factoryProvider;

        #endregion private fields

        #region constructor

        protected TestFixtureBase()
        {
            var logger = new DefaultLogger();
            var configProvider = new MultiSessionFactoryConfigurationProvider(logger);
            configProvider.AfterConfigure += HookupMappings;
            _factoryProvider = new MultiSessionFactoryProvider(logger, configProvider);
        }

        #endregion constructor

        #region public properties

        public static IDictionary<string, string> Properties { get; set; }

        private static PersistenceModel Model { get; set; }

        protected static IUnitOfWork HpcUnitOfWorkTest
        {
            get { return _hpcUnitOfWork ?? (_hpcUnitOfWork = GetUnitOfWork()); }
        }

        protected static IUnitOfWork JdeUnitOfWorkTest
        {
            get { return _jdeUnitOfWork ?? (_jdeUnitOfWork = GetUnitOfWork(ApplicationSetting.JdeDbSessionName)); }
        }

        protected static IMultiSessionUnitOfWork HadrianUnitOfWork
        {
            get { return _multiUnitOfWork ?? (_multiUnitOfWork = GetHadrianUnitOfWork()); }
        }

        #endregion public properties

        #region public methods

        protected static void TearDownUnitOfWork()
        {

            if (_hpcUnitOfWork != null)
            {
                _hpcUnitOfWork.Dispose();
                _hpcUnitOfWork = null;
            }

            if (JdeUnitOfWorkTest != null)
            {
                JdeUnitOfWorkTest.Dispose();
                _jdeUnitOfWork = null;
            }

            _multiUnitOfWork = null;
        }

        #endregion public methods

        #region private methods

        private static void HookupMappings(object sender, ConfiguredEventArgs args)
        {
            var name = args.ConfigurationName;
            var cfg = args.Configuration;

            cfg.SessionFactory().GenerateStatistics();

            //if (Properties == null)
            //{
            //    cfg.Configure();
            //}
            //else
            //{
            //    cfg.AddProperties(Properties);
            //}

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

        /// <summary>
        /// Gets or sets the default session factory.
        /// </summary>
        /// <value>The default session factory.</value>
        private static ISessionFactoryProvider DefaultFactoryProvider
        {
            get
            {
                return _factoryProvider;
            }
        }

        private static ISessionFactory HpcSessionFactory
        {
            get
            {
                if (_hpcSessionFactory == null)
                {
                    _hpcSessionFactory = DefaultFactoryProvider.GetFactory(ApplicationSetting.HpcDbSessionName);
                }

                if (_hpcSessionFactory != null)
                {
                    return _hpcSessionFactory;
                }

                var errorMsg = string.Format("Cannot find HPC session factory {0}", ApplicationSetting.HpcDbSessionName);
                throw new ArgumentException(errorMsg);
            }
        }

        private static ISessionFactory JdeSessionFactory
        {
            get
            {
                if (_jdeSessionFactory == null)
                {
                    _jdeSessionFactory = DefaultFactoryProvider.GetFactory("nhfactoryJde");
                }

                if (_jdeSessionFactory != null)
                {
                    return _jdeSessionFactory;
                }

                throw new ArgumentException("Cannot find JDE session factory");
            }
        }

        private static IUnitOfWork GetUnitOfWork(string dataSource = ApplicationSetting.HpcDbSessionName)
        {
            var src = dataSource.Trim();
            ISessionFactory factory;
            switch (src)
            {
                case ApplicationSetting.JdeDbSessionName:
                    factory = JdeSessionFactory;
                    return new NhUnitOfWork(factory.OpenSession(), factory.OpenStatelessSession());

                default:
                    factory = HpcSessionFactory;
                    return new NhUnitOfWork(factory.OpenSession(), factory.OpenStatelessSession());
            }
        }

        private static IMultiSessionUnitOfWork GetHadrianUnitOfWork()
        {
            var uows = new Dictionary<string, IUnitOfWork>
            {
                {ApplicationSetting.HpcDbSessionName, HpcUnitOfWorkTest},
                {ApplicationSetting.JdeDbSessionName, JdeUnitOfWorkTest}
            };

            var muow = new NhMultiSessionUnitOfWork
            {
                UnitOfWorks = uows
            };

            return muow;
        }

        #endregion private methods
    }
}