using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Grpc.Reflection;
using Grpc.AspNetCore;
using System;
using TestingWASM.Server.Api.Form.Services;
using TestingWASM.Shared.Context;

namespace TestingWASM.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; private set; }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
         {
             builder.AddDebug();
         }
 );

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            HostingEnvironment = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddGrpcReflection();


            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        ;
            }));

            services.AddResponseCaching();
            services.AddDbContext<pocContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:pocdb"]);
                options.UseLoggerFactory(loggerFactory);
                options.EnableSensitiveDataLogging();
            });

            services.AddScoped<IpocContext>(provider => provider.GetService<pocContext>());
            services.AddHttpContextAccessor();

            services.AddScoped<FormEntryService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
            services.AddControllersWithViews();
            services.AddRazorPages();
            //services.AddTransient<IFormReadService, FormReadService>();
            //services.AddTransient<IFormService, FormService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //     app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
               // app.UseHsts();
            }
           
            app.UseHsts();
            
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
            app.UseCors();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapGrpcService<FormEntryService>().EnableGrpcWeb().RequireCors("AllowAll").AllowAnonymous();

                if (env.IsDevelopment())
                {
                    endpoints.MapGrpcReflectionService();
                }
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}