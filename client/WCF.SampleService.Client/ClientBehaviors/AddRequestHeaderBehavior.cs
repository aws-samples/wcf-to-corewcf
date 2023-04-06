// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCF.SampleService.Client.ClientBehaviors
{
    public class AddRequestHeaderBehavior : IEndpointBehavior, IClientMessageInspector
    {
        #region IEndpointBehavior methods
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(this);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
        #endregion

        #region IClientMessageInspector methods
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            string headerNamespace = "AppHeaderNamespace";
            string clientIdHeader = "ClientId";

            MessageHeader header = MessageHeader.CreateHeader(clientIdHeader, headerNamespace, "12345");
            request.Headers.Add(header);

            return null;
        }
        #endregion
    }
}
