using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Example1.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var cert = new X509Certificate2(Path.Combine("Certs/server2.pfx"), "1111");
                    webBuilder.UseStartup<Startup>()
                      .ConfigureKestrel(options =>
                      {
                          options.Limits.MinRequestBodyDataRate = null;
                          options.ListenLocalhost(50051, listenOptions =>
                          {
                              listenOptions.UseHttps(cert);
                              listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                              listenOptions.UseConnectionLogging();

                          });
                          //var cert = new X509Certificate2(Path.Combine("server.pfx"), "1111");
                          //options.ConfigureHttpsDefaults(o =>
                          //{
                          //    o.ServerCertificate = cert;
                          //    o.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                          //});

                      })
                       .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                        .MinimumLevel.Verbose()
                        .Enrich.FromLogContext()
                        .WriteTo.File("../_Example1ServerLogs.txt")
                        .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                    );
                });
    }
}
