// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

#if NETFRAMEWORK
using CoreWCF.SampleService.Test.Behaviors.HeaderValidationBehavior;
using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using WCF.SampleService.Behaviors.HeaderValidationBehavior;

namespace WCF.SampleService.Test
{
    internal class TestServiceHostingEnvironment<T> : IDisposable
    {
        ServiceHost serviceHost;
        public TestServiceHostingEnvironment(string serviceUrl, IServiceBehavior item, bool addEndPointBehavior = false)
        {
            try
            {
                var uri = new Uri(serviceUrl);
                serviceHost = new ServiceHost(typeof(T), uri);

                serviceHost.Description.Behaviors.Add(item);

                if (addEndPointBehavior)
                {
                    serviceHost.AddServiceEndpoint(
                        typeof(HeaderValidationBehaviorTests.ITestService),
                        new BasicHttpBinding(),
                        serviceUrl);

                    serviceHost.Description.Endpoints.ToList().ForEach(endpoint =>
                    {
                        endpoint.EndpointBehaviors.Add(new HeaderValidationBehavior());
                    });
                }

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
            // Create a channel factory.
            BasicHttpBinding myBinding = new BasicHttpBinding();

            EndpointAddress myEndpoint = new EndpointAddress(serviceUrl);

            ChannelFactory<Type> myChannelFactory = new ChannelFactory<Type>(myBinding, myEndpoint);

            return myChannelFactory.CreateChannel();
        }
    }
}
#endif