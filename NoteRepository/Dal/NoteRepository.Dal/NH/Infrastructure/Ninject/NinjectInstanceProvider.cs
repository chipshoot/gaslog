using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Ninject;

namespace NoteRepository.Dal.NH.Infrastructure.Ninject
{
    public class NinjectInstanceProvider : IInstanceProvider
    {
        #region private fields

        private readonly Type _serviceType;
        private readonly IKernel _kernel;

        #endregion private fields

        #region constructors

        public NinjectInstanceProvider(IKernel kernel, Type serviceType)
        {
            _kernel = kernel;
            _serviceType = serviceType;
        }

        #endregion constructors

        #region implement interface IInstanceProvider

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return _kernel.Get(_serviceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
        }

        #endregion implement interface IInstanceProvider
    }
}