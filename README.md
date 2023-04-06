## Reference implementation - Modernizing WCF Service to CoreWCF 

This is a reference implementation for modernizing legacy WCF services to CoreWCF.

This repository includes 2 versions of WCF services:
* Legacy WCF service, built on .NET Framework 4.7.2
* Modernized version of the legacy WCF service, built on .NET 6 and CoreWCF 

The WCF services are exposed using BasicHttpBinding binding. 
The application uses custom behaviors on both client and service side, making it more similar to a real-world WCF application. 
In legacy WCF service, Behaviors have been configured through `web.config` or `app.config` file using classes that inherit from `BehaviorExtensionElement`.

Structure of the repository:
```
- src
  - client
      - WCF.SampleService.Client
  - legacy-wcf-service
      - WCF.SampleService
      - WCF.SampleService.Test
  - modernized-wcf-service
      - CoreWCF.SampleService
      - CoreWCF.SampleService.Test
```

- `src\client` contains client application, built with .NET Framework 4.7.2
- `src\legacy-wcf-service` contains a legacy WCF service, built with .NET Framework 4.7.2
- `src\modernized-wcf-service` contains the modernized version of the legacy WCF service, built using CoreWCF and .NET 6. 

  - The modernized solution supports backward compatibility, i.e., it can be built for .NET Framework 4.7.2 and for .NET 6. 
  - To build for .NET Framework 4.7.2, use the 'Debug' or 'Release' configuration. 
  - To build for .NET 6, use the 'Debug6' or 'Release6' configuration.
  - Unit tests in this solution would also work for .NET Framework 4.7.2 and for .NET 6. Choose the appropriate build configuration.

### How to build & run

- legacy-wcf-service
  - Build and publish, using Debug or Release configuration. 
  - Host on IIS

- modernized-wcf-service 
  - Contains both legacy WCF and CoreWCF implementations.
  - To build and run legacy WCF service (.NET Framework), use Debug or Release configuration. Host on IIS.
  - To build and run CoreWCF service (.NET 6), use Debug6 or Release6 configuration. Self-hosted on kestrel.

- client application
   - Edit App.config and update endpoint addresses of the WCF services
   - Build and run in Debug or Release mode.   
   
### Security considerations

This is just a reference implementation and should not be used as-is for production grade code. 

The following security aspects should be addressed:
- Hard-coded credentials
  - The sample code in the Program.cs client application contains hard-coded credentials for demonstration purposes.
  - Use a secret management solution like AWS Secrets Manager, for storing secure configs.
- Authentication / Authorization
  - The sample AuthService implementation returns true for any provided credentials, which is just a placeholder code.
  - WCF Authentication: https://learn.microsoft.com/en-us/dotnet/framework/wcf/feature-details/authentication-in-wcf
  - WCF Authorization: https://learn.microsoft.com/en-us/dotnet/framework/wcf/feature-details/authorization-in-wcf
- Message and Transport security
  - Use HSTS (HTTP Strict Transport Security) to prevent man-in-the-middle attacks.
  - Common security scenarios for WCF: https://learn.microsoft.com/en-us/dotnet/framework/wcf/feature-details/common-security-scenarios
- Input validation
  - Inputs to services should be validated early instead of relying on exceptions.
- Configuration
  - Prefer to use AWS Systems Manager Parameter Store for configuration entries.  

### References
- [Configuring and Extending the Runtime with Behaviors](https://learn.microsoft.com/en-us/dotnet/framework/wcf/extending/configuring-and-extending-the-runtime-with-behaviors)
- [Writing a WCF Message Inspector](https://weblogs.asp.net/paolopia/writing-a-wcf-message-inspector)

## Security

See [CONTRIBUTING](CONTRIBUTING.md#security-issue-notifications) for more information.

## License

This library is licensed under the MIT-0 License. See the LICENSE file.

