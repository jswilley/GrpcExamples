#  Grpc Examples

This is my attempt to understand [grpc](https://grpc.io/) and how I might use in .net projects with an orientation towards [Blazor](https://docs.microsoft.com/en-us/aspnet/core/blazor/host-and-deploy/webassembly?view=aspnetcore-5.0) and Apis.  I have created three examples.  


## Example 1

After a little reading, I used two blog posts to create the initial project:

 1. [https://azure.github.io/AppService/2021/03/15/How-to-use-gRPC-Web-with-Blazor-WebAssembly-on-App-Service.html](https://azure.github.io/AppService/2021/03/15/How-to-use-gRPC-Web-with-Blazor-WebAssembly-on-App-Service.html)
 2. [https://damienbod.com/2019/03/06/security-experiments-with-grpc-and-asp-net-core-3-0/](https://damienbod.com/2019/03/06/security-experiments-with-grpc-and-asp-net-core-3-0/)

I diverged from both examples.  The Blazor client calls a free standing Api that has both the Weather and the Greeter endpoints.  User are not authenticated but identity server  validator tokens  are used for authorization.   The Blazor client has a linked copy of the protobuf's for greeter and weather with only the client side generated.  These endpoints are wrapped by a GrpcApiService  that acquires the bearer token prior to the grpc call.  

One thing to note on the server side is the code does the token authorization but allows for anonymous users -- see the attributes on the services.  Serilog is used as the logger.



## Example 2

A more complex protobuf with  entityframework and automapper(not working yet).       This is just a hosted Blazor WASM application (split into 3 projects client/server/shared).  
The code generates a form based on on metadata.  It does not save :).    Some of the protobuf elements include:

 - Dates  (i.e., google.protobuf.Timestamp)
 - Nullables (e.g., google.protobuf.StringValue)
 - Nested Messages
 - Lists
 - Versioning
 - Enums

The Automapper implementation has a number of useful conversions around enums and dates (See the Server side file: FormMappingProfilev2).


External Testing (similar to postman can be done with Grpcui). See this article: [https://anthonygiretti.com/2021/01/17/grpc-asp-net-core-5-discover-grpcui-the-gui-alternative-to-grpcurl/](https://anthonygiretti.com/2021/01/17/grpc-asp-net-core-5-discover-grpcui-the-gui-alternative-to-grpcurl/).
