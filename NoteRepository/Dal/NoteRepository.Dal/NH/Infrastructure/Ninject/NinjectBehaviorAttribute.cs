using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Ninject;

namespace NoteRepository.Dal.NH.Infrastructure.Ninject
{
    public class NinjectBehaviorAttribute : Attribute, IServiceBehavior
    {
        #region implement interface IServiceBehavior

        public void AddBindingParameters(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameterCollection)
        {
        }

        public void ApplyDispatchBehavior(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
            Type serviceType = serviceDescription.ServiceType;
            IInstanceProvider instanceProvider = new NinjectInstanceProvider(new StandardKernel(), serviceType);

            foreach (var channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var dispatcher = (ChannelDispatcher) channelDispatcherBase;
                foreach (var endpointDispatcher in dispatcher.Endpoints)
                {
                    var dispatchRuntime = endpointDispatcher.DispatchRuntime;
                    dispatchRuntime.InstanceProvider = instanceProvider;
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        #endregion implement interface IServiceBehavior
    }
}