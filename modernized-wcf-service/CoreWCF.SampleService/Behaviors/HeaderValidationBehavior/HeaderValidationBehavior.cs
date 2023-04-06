// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

using System;
#if NETFRAMEWORK
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;
#else
    using CoreWCF;
    using CoreWCF.Channels;
    using CoreWCF.Description;
    using CoreWCF.Dispatcher;
#endif

namespace WCF.SampleService.Behaviors.HeaderValidationBehavior
{
    public class HeaderValidationBehavior : IEndpointBehavior
    {
#region IEndpointBehavior methods
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new HeaderInspector());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
#endregion
    }
}