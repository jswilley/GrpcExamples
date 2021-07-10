using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestingWASM.Services;
using static TestingWASM.Services.FormEntry;

namespace TestingWASM.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSingleton(services =>
            {
                var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
                var baseUri = services.GetRequiredService<NavigationManager>().BaseUri;
                var channel = GrpcChannel.ForAddress(baseUri, new GrpcChannelOptions { HttpClient = httpClient });
                return new FormEntryClient(channel).GetFormAsync(new FormEntryRequest() { FormTypeId = 1 });
            });

            //var builder = WebAssemblyHostBuilder.CreateDefault(args);
            //builder.RootComponents.Add<App>("#app");
            ////using var channel = GrpcChannel.ForAddress("https://localhost:44366");

            ////var client = new FormEntryClient(channel).GetFormAsync(new FormEntryRequest() { FormTypeId = 1 });

            //builder.Services.AddGrpcClient<FormEntryClient>(o =>
            //{
            //    o.Address = new Uri("https://localhost:44366");


            //});

            //  builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            //builder.Services.AddScoped<GrpcChannel>(services =>
            //{
            //    // This switch must be set before creating the GrpcChannel/HttpClient.

            //    //var grpcWebHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
            //    var channel = GrpcChannel.ForAddress(new Uri("https://localhost:44366"), new GrpcChannelOptions { HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()) });

            //    return channel;
            //});
            ////https://docs.microsoft.com/en-us/aspnet/core/grpc/clientfactory?view=aspnetcore-5.0
            //builder.Services.AddGrpcClient<FormEntryClient>(o =>
            //{
            //    o.Address = new Uri("https://localhost:44366");


            //});
            //.ConfigurePrimaryHttpMessageHandler(() =>
            // {
            //     var handler = new HttpClientHandler();
            //     handler.ClientCertificates.Add(LoadCertificate());
            //     return handler;
            // });

            builder.Services.AddScoped<IFormClientService, FormClientService>();


            await builder.Build().RunAsync();
        }
    }
}
