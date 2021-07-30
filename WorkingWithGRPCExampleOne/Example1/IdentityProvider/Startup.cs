// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace IdentityProvider
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
                    .AddNewtonsoftJson();

            //add identity server 4
            var builder = services.AddIdentityServer(
                         options =>
                         {
                             options.Events.RaiseErrorEvents = true;
                             options.Events.RaiseFailureEvents = true;
                             options.Events.RaiseInformationEvents = true;
                             options.Events.RaiseSuccessEvents = true;
                         }
               )
           
            .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(Config.Apis)
             .AddInMemoryClients(Config.Clients);
            

        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            //IS4 start page
            string apiResources = "<div><hr>Registered ApiResouces:<ul>";
            List<IdentityServer4.Models.ApiResource> apires = Config.Apis.ToList();
            foreach (IdentityServer4.Models.ApiResource ar in apires)
            {
                apiResources += "<li>" + ar.Name + "</li>";
            }
            apiResources += "</ul></div>";

            string clients = "<div><hr>Registered Clients:<ul>";
            List<IdentityServer4.Models.Client> clies = Config.Clients.ToList();
            foreach (IdentityServer4.Models.Client cl in clies)
            {
                clients += "<li>" + cl.ClientId + "</li>";
            }
            clients += "</ul></div>";

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(@"
					 <html><head><link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css' integrity='sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb' crossorigin='anonymous'><link rel='stylesheet' href='https://use.fontawesome.com/releases/v5.3.1/css/all.css' integrity='sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU' crossorigin='anonymous'></head><body>
					 <div class='jumbotron'>    
						<img class='thumbnail img-responsive' src='https://identityserver4.readthedocs.io/en/latest/_images/logo.png' />
						<h3>IdentityServer service is running!</h3>
						Supported by IdentityServer4. <br/>IdentityServer4 is an OpenID Connect and OAuth 2.0 framework for ASP.NET Core.
					</div>" +
                    apiResources + clients +
                    @"
					<hr><a href='/.well-known/openid-configuration'>Get IS4 configuration</a>                    					
					<hr><a href='https://identityserver4.readthedocs.io/en/latest/'>Documentation</a> 
					<hr>Full version of VSIX template for download at <a class='btn btn-outline-primary' role='button' href='http://www.anasoft.net/apincore'><b>www.anasoft.net/apincore</b></a>
					</body></html>
					", System.Text.Encoding.UTF8);
            });
        }
    }
}