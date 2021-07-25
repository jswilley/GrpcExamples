using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Example1.Shared;
using IdentityServer4.AccessTokenValidation;
using Newtonsoft.Json;
using Serilog;

namespace Example1.Server
{
    public class Startup
    {

        private string stsServer = "https://localhost:44352";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("protectedScope", policy =>
                {
                    policy.RequireClaim("scope", "grpc_protected_scope");
                });
            });

            services.AddAuthorizationPolicyEvaluator();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = stsServer;
                    options.ApiName = "ProtectedGrpc";
                    options.ApiSecret = "grpc_protected_secret";
                    options.RequireHttpsMetadata = false;
                });

            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
            });

            services.AddMvc()
               .AddNewtonsoftJson();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

           // must be added after UseRouting and before UseEndpoints
app.UseGrpcWeb();

            app.UseEndpoints(endpoints =>
            {
                // map to and register the gRPC service
                endpoints.MapGrpcService<WeatherService>().EnableGrpcWeb().RequireAuthorization("protectedScope"); 
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
