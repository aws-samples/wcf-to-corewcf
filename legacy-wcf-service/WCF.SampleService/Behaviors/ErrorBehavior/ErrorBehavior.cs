// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using WCF.SampleService.Loggers;

namespace WCF.SampleService.Behaviors.ErrorBehavior
{
    /// <summary>
    /// ErrorBehavior is a ServiceBehavior that attaches a custom error handler to all the endpoints in the service
    /// </summary>
    public sealed class ErrorBehavior : IServiceBehavior
    {
        Type errorHandlerType;
        private IFileLogger _logger;

        public ErrorBehavior(Type errorHandlerType, IFileLogger logger)
        {
            this.errorHandlerType = errorHandlerType;
            _logger = logger;
        }

        public Type ErrorHandlerType
        {
            get { return this.errorHandlerType; }
        }

        void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
        {
        }

        void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
        {
        }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
        {
            IErrorHandler errorHandler;

            try
            {
                errorHandler = (IErrorHandler)Activator.CreateInstance(errorHandlerType, _logger);
            }
            catch (MissingMethodException e)
            {
                throw new ArgumentException("The errorHandlerType specified in the ErrorBehavior constructor must have a public empty constructor.", e);
            }
            catch (InvalidCastException e)
            {
                throw new ArgumentException("The errorHandlerType specified in the ErrorBehavior constructor must implement System.ServiceModel.Dispatcher.IErrorHandler.", e);
            }

            // Attach error handler to all the endpoints (via Channel Dispatcher) in a service host
            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;
                channelDispatcher.ErrorHandlers.Add(errorHandler);
            }
        }
    }
}
