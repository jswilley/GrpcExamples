using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Ex1Shared;
using System;

namespace Ex1GrpcClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");


            builder.Services.AddHttpClient("api", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44371");
                client.DefaultRequestHeaders.Accept.Clear();
                //  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpContentMediaTypes.JSON));
                TimeSpan.FromSeconds(60);
            });
            //.AddHttpMessageHandler(sp =>
            //{
            //    var handler = sp.GetService<AuthorizationMessageHandler>()
            //        .ConfigureHandler(
            //            authorizedUrls: new[] { "https://localhost:44371" },
            //            scopes: new[] { "example1" });

            //    return handler;
            //});

            builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("api"));
            ////Register a Typed Instance of HttpClientFactory for AuthService 
            //services.AddHttpClient<IAuthServerConnect, AuthServerConnect>();

            //// Register the DiscoveryCache in DI and will use the HttpClientFactory to create clients. Cached for 24hrs by default
            //services.AddSingleton<IDiscoveryCache>(r =>
            //{
            //    IHttpClientFactory factory = r.GetRequiredService<IHttpClientFactory>();


            //    return new DiscoveryCache(config["EndpointSettings:AuthServer"], () => factory.CreateClient());
            //});



            builder.Services.AddSingleton(services =>
            {
                var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
                var baseUri = "https://localhost:44371";
                var channel = GrpcChannel.ForAddress(baseUri, new GrpcChannelOptions { HttpClient = httpClient });
                return new WeatherForecasts.WeatherForecastsClient(channel);
            });

            await builder.Build().RunAsync();
        }
    }
}
