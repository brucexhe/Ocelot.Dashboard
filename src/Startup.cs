using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.Authentication.Middleware;
using Ocelot.Authorisation.Middleware;
using Ocelot.Cache.Middleware;
using Ocelot.Configuration.Creator;
using Ocelot.Configuration.Repository;
using Ocelot.DependencyInjection;
using Ocelot.DownstreamRouteFinder.Middleware;
using Ocelot.DownstreamUrlCreator.Middleware;
using Ocelot.Errors.Middleware;
using Ocelot.Headers.Middleware;
using Ocelot.LoadBalancer.Middleware;
using Ocelot.Middleware.Pipeline;
using Ocelot.Request.Middleware;
using Ocelot.Requester.Middleware;
using Ocelot.RequestId.Middleware;
using Ocelot.Responder.Middleware; 
using Ocelot.Provider.Consul;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Ocelot.Dashboard.Service;

namespace Ocelot.Dashboard
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddSingleton<ConsulService>();

            services.AddOcelot().AddConsul();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //FOR ocelot request
            app.UseOcelotWhenRouteMatch((ocelotBuilder, pipelineConfiguration) =>
            {
                // This is registered to catch any global exceptions that are not handled
                // It also sets the Request Id if anything is set globally
                ocelotBuilder.UseExceptionHandlerMiddleware();
                // This is registered first so it can catch any errors and issue an appropriate response
                ocelotBuilder.UseResponderMiddleware();
                ocelotBuilder.UseDownstreamRouteFinderMiddleware();
                ocelotBuilder.UseDownstreamRequestInitialiser();
                ocelotBuilder.UseRequestIdMiddleware();
                ocelotBuilder.UseMiddleware<ClaimsToHeadersMiddleware>();
                ocelotBuilder.UseAuthorisationMiddleware();
                ocelotBuilder.UseAuthenticationMiddleware();
                ocelotBuilder.UseLoadBalancingMiddleware();
                ocelotBuilder.UseDownstreamUrlCreatorMiddleware();
                ocelotBuilder.UseOutputCacheMiddleware();
                ocelotBuilder.UseMiddleware<HttpRequesterMiddleware>();
                // cors headers
                ocelotBuilder.UseMiddleware<CorsMiddleware>();
            });


            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
