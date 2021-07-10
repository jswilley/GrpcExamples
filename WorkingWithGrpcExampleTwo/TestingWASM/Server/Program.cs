using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Security.Authentication;

namespace TestingWASM.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)

.ConfigureWebHostDefaults(webBuilder =>
{
webBuilder.ConfigureKestrel((context, serverOptions) =>
{
serverOptions.Limits.MaxConcurrentConnections = 100;
serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
serverOptions.Limits.MaxRequestBodySize = 10 * 1024;
serverOptions.Limits.MinRequestBodyDataRate =
new MinDataRate(bytesPerSecond: 100,
gracePeriod: TimeSpan.FromSeconds(10));
serverOptions.Limits.MinResponseDataRate =
new MinDataRate(bytesPerSecond: 100,
gracePeriod: TimeSpan.FromSeconds(10));
serverOptions.Listen(IPAddress.Loopback, 44366,
    listenOptions =>
    {
        listenOptions.UseHttps("test.pfx",
            "admin");
    });
    serverOptions.ConfigureHttpsDefaults(optx => { optx.SslProtocols = SslProtocols.None; });
    serverOptions.ConfigureEndpointDefaults(eopt => { eopt.Protocols = HttpProtocols.Http2; });

serverOptions.Limits.KeepAliveTimeout =
TimeSpan.FromMinutes(2);
serverOptions.Limits.RequestHeadersTimeout =
TimeSpan.FromMinutes(1);
serverOptions.Limits.Http2.MaxStreamsPerConnection = 100;
serverOptions.Limits.Http2.HeaderTableSize = 4096;
serverOptions.Limits.Http2.MaxFrameSize = 16384;
serverOptions.Limits.Http2.MaxRequestHeaderFieldSize = 8192;
serverOptions.Limits.Http2.InitialConnectionWindowSize = 131072;
   

});
webBuilder.UseStartup<Startup>();
});
        }
    }
}