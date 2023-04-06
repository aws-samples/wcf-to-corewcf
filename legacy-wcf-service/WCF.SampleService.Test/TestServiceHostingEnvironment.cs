// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WCF.SampleService.Test
{
    internal class TestServiceHostingEnvironment<T>:IDisposable
    {
        ServiceHost serviceHost;

        public TestServiceHostingEnvironment(string serviceUrl, IServiceBehavior item)
        {
            try
            {
                var uri = new Uri(serviceUrl);
                serviceHost = new ServiceHost(typeof(T), uri);
                serviceHost.Description.Behaviors.Add(item);
                serviceHost.Open();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public void Dispose()
        {
            serviceHost.Close();
        }

        public Type GetChannel<Type>(string serviceUrl)
        {
            BasicHttpBinding binding = new BasicHttpBinding();

            EndpointAddress endpoint = new EndpointAddress(serviceUrl);

            ChannelFactory<Type> channelFactory = new ChannelFactory<Type>(binding, endpoint);

            return channelFactory.CreateChannel();
        }
    }
}
