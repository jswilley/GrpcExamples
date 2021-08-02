using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;


namespace Ex1GrpcClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");


             //.AddHttpMessageHandler(sp =>
            //{
            //    var handler = sp.GetService<AuthorizationMessageHandler>()
            //        .ConfigureHandler(
            //            authorizedUrls: new[] { "https://localhost:44371" },
            //            scopes: new[] { "grpc_protected_scope" });

            //    return handler;
            //});

            builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("api"));
            ////Register a Typed Instance of HttpClientFactory for AuthService 
            builder.Services.AddHttpClient<IAuthServerConnect, AuthServerConnect>();
            //Register custom Bearer Token Handler. The DelegatingHandler has to be registered as a Transient Service
            builder.Services.AddTransient<ProtectedApiBearerTokenHandler>();
            builder.Services.AddTransient<GrpcApiService>();
            builder.Services.AddSingleton<ApiTokenInMemoryClient>();

            // Register the DiscoveryCache in DI and will use the HttpClientFactory to create clients. Cached for 24hrs by default
            builder.Services.AddSingleton<IDiscoveryCache>(r =>
            {
                IHttpClientFactory factory = r.GetRequiredService<IHttpClientFactory>();


                return new DiscoveryCache("https://localhost:5000", () => factory.CreateClient());
            });

            //builder.Services.AddHttpClient("api", client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:44371");
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    //  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpContentMediaTypes.JSON));
            //    TimeSpan.FromSeconds(60);
            //}).AddHttpMessageHandler<ProtectedApiBearerTokenHandler>();
            //https://code-maze.com/authenticationstateprovider-blazor-webassembly/
            //https://docs.microsoft.com/en-us/aspnet/core/grpc/authn-and-authz?view=aspnetcore-5.0
            //C:\Users\willeyj\Downloads\Blazorgrpcid\Blazor-WASM-Identity-gRPC-Main
            //C:\Users\willeyj\Downloads\BlazorGrpcAuth-master\BlazorGrpcAuth-master
            var handler = new HttpClientHandler();
            builder.Services.AddHttpClient("grpc", services =>
             {

             }).ConfigurePrimaryHttpMessageHandler(() => handler); //.AddHttpMessageHandler<ProtectedApiBearerTokenHandler>(); ;

            await builder.Build().RunAsync();
        }
    }
}
