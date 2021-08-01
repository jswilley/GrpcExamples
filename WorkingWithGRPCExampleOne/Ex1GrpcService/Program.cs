using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Ex1GrpcService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                     var cert = new X509Certificate2("Certs\\server2.pfx", "");
                    webBuilder.UseStartup<Startup>()
                      //.ConfigureKestrel(options =>
                      //{
                      //    options.Limits.MinRequestBodyDataRate = null;
                      //    options.ListenLocalhost(44371, listenOptions =>
                      //    {
                      //        listenOptions.UseHttps(cert);
                      //        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                      //        listenOptions.UseConnectionLogging();

                      //    });
                          

                      //})
                       .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                        .MinimumLevel.Verbose()
                        .Enrich.FromLogContext()
                        .WriteTo.File("../_Ex1GrpcServer.txt")
                        .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                    );
                });
    }
}