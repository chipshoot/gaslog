using FluentNHibernate;
using NHibernate;
using NHibernate.Cfg;
using NoteRepository.Common.Utility.Dal;
using NoteRepository.Common.Utility.LogService;
using NoteRepository.Dal.NH;
using NoteRepository.Dal.NH.Infrastructure;
using NoteRepository.Dal.NH.Infrastructure.SessionMan;
using System;
using System.Collections.Generic;

namespace NoteRepository.Dal.Tests
{
    /// <summary>
    /// The class is used to setup testing environment for NHibernate to connect
    /// database, also it setup the all property need to be inject
    /// </summary>
    public class TestFixtureBase
    {
        #region private fields

        private static ISessionFactory _sessionFactory;

        private static IUnitOfWork _unitOfWork;

        private static ISessionFactoryProvider _factoryProvider;

        #endregion private fields

        #region constructor

        protected TestFixtureBase()
        {
            var logger = new DefaultLogger();
            var configProvider = new ConfigurationProvider(logger);
            configProvider.AfterConfigure += HookupMappings;
            _factoryProvider = new SessionFactoryProvider(logger, configProvider);
        }

        #endregion constructor

        #region public properties

        public static IDictionary<string, string> Properties { get; set; }

        private static PersistenceModel Model { get; set; }

        protected static IUnitOfWork UnitOfWorkTest => _unitOfWork ?? (_unitOfWork = GetUnitOfWork());

        #endregion public properties

        #region public methods

        protected static void TearDownUnitOfWork()
        {
            if (_unitOfWork == null)
            {
                return;
            }

            _unitOfWork.Dispose();
            _unitOfWork = null;
        }

        #endregion public methods

        #region private methods

        private static void HookupMappings(object sender, ConfiguredEventArgs args)
        {
            var cfg = args.Configuration;
            cfg.SessionFactory().GenerateStatistics();
            Model = new DefaultModel();
            Model.Configure(cfg);
        }

        /// <summary>
        /// Gets or sets the default session factory.
        /// </summary>
        /// <value>The default session factory.</value>
        private static ISessionFactoryProvider DefaultFactoryProvider => _factoryProvider;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = DefaultFactoryProvider.GetFactory();
                }

                if (_sessionFactory != null)
                {
                    return _sessionFactory;
                }

                var errorMsg = "Cannot find HPC session factory ";
                throw new ArgumentException(errorMsg);
            }
        }

        private static IUnitOfWork GetUnitOfWork()
        {
            var factory = SessionFactory;
            return new NhUnitOfWork(factory.OpenSession(), factory.OpenStatelessSession());
        }

        #endregion private methods
    }
}