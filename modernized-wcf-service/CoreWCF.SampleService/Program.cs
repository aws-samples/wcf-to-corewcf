// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

#if !NETFRAMEWORK

using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using WCF.SampleService.Behaviors.ErrorBehavior;
using WCF.SampleService.Behaviors.HeaderValidationBehavior;
using WCF.SampleService.Contracts;
using WCF.SampleService.Loggers;
using WCF.SampleService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add WSDL support
builder.Services.AddServiceModelServices().AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

// Register dependencies
builder.Services.AddSingleton<IFileLogger, FileLogger>(serviceProvider => new FileLogger(builder.Configuration["appSettings:logFilePath"]));

var app = builder.Build();

app.UseServiceModel(builder =>
{
    builder.AddService<AuthService>((serviceOptions) => { })
    .AddServiceEndpoint<AuthService, IAuthService>(new BasicHttpBinding(), "/Services/AuthService.svc");

    builder.AddService<CalculatorService>((serviceOptions) => { })
    .AddServiceEndpoint<CalculatorService, ICalculatorService>(new BasicHttpBinding(), "Services/CalculatorService.svc");

    builder.ConfigureAllServiceHostBase(serviceHost =>
    {
        // Add service behaviors
        var fileLogger = app.Services.GetRequiredService<IFileLogger>();
        serviceHost.Description.Behaviors.Add(new ErrorBehavior(typeof(GlobalErrorHandler), fileLogger));

        // Add endpoint behaviors
        serviceHost.Description.Endpoints.ToList().ForEach(endpoint =>
        {
            endpoint.EndpointBehaviors.Add(new HeaderValidationBehavior());
        });
    });
});

var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
serviceMetadataBehavior.HttpGetEnabled = true;

app.Run();

#else

// Note: SDK style project requires a Main method, so keeping this class just to prevent compilation errors.
public class Program
{
    public static void Main(string[] args)
    {

    }
}
#endif
