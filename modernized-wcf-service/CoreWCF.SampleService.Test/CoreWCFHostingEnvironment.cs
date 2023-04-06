// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

#if !NETFRAMEWORK
using System;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Linq;
using SSM = System.ServiceModel;
using System.Collections.Generic;

namespace CoreWCF.SampleService.Test
{
    public class CoreWCFHostingEnvironment:IDisposable
    {
        private IWebHost host;
        public string EndpointBase { get; private set; }

        public CoreWCFHostingEnvironment(IWebHost host, string endpointBase)
        {
            this.host = host;
            EndpointBase = endpointBase;
        }

        public void Dispose()
        {
            host.Dispose();
        }

        public static CoreWCFHostingEnvironment Run<T>(string baseUrl)
        {
            try
            {
                var builder = new WebHostBuilder()
                .UseUrls(baseUrl)
                .UseKestrel();

                builder.UseStartup(typeof(T));

                var host = builder.Build();
                var task = host.RunAsync();
                try
                {
                    var server = (IServer)host.Services.GetService(typeof(IServer));
                    var addressFeature = server.Features.Get<IServerAddressesFeature>();
                    return new CoreWCFHostingEnvironment(host, addressFeature.Addresses.Single());
                }
                catch (ObjectDisposedException)
                {
                    throw task.Exception.InnerExceptions[0];
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public Type GetChannel<Type>(string serviceUrl)
        {
            // Create a channel factory.
            SSM.BasicHttpBinding myBinding = new SSM.BasicHttpBinding();
            SSM.EndpointAddress myEndpoint = new SSM.EndpointAddress(serviceUrl);
            SSM.ChannelFactory<Type> myChannelFactory = new SSM.ChannelFactory<Type>(myBinding, myEndpoint);

            return myChannelFactory.CreateChannel();
        }
    }

    public static class CoreWCFStartupExtensions
    {
        public static void AddService<TService>(this IServiceBuilder serviceBuilder, IEnumerable<IServiceBehavior> behaviors) where TService : class
        {
            serviceBuilder.AddService<TService>(serviceOptions => {
                serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
            });
            serviceBuilder.ConfigureServiceHostBase<TService>(serviceHost =>
            {
                if (behaviors != null)
                {
                    foreach (var behavior in behaviors)
                        serviceHost.Description.Behaviors.Add(behavior);
                }
            });
        }

        public static void AddEndpoint<TService, TContract>(this IServiceBuilder serviceBuilder, IEnumerable<IEndpointBehavior> behaviors, Binding binding, string url, string path) where TService : class
        {
            serviceBuilder.AddServiceEndpoint<TService, TContract>(binding, url, endpoint =>
            {
                if (behaviors != null)
                {
                    foreach (var behavior in behaviors)
                        endpoint.EndpointBehaviors.Add(behavior);
                }
            });
        }
    }
}
#endif