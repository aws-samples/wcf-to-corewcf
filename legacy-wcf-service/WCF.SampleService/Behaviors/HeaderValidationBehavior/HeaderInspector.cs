// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace WCF.SampleService.Behaviors.HeaderValidationBehavior
{
    public class HeaderInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            string headerNamespace = "AppHeaderNamespace";
            string clientIdHeader = "ClientId";

            int headerIndex = request.Headers.FindHeader(clientIdHeader, headerNamespace);

            if (headerIndex == -1)
            {
                throw new FaultException("ClientId was not found the request.");
            }

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }
    }
}