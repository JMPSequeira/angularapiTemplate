using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace angularapiTemplate.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            // This ensures the app will serve the static files
            if (env.IsProduction())
                app.UseFileServer();

            app.UseHttpsRedirection();

            app.UseMvc();

            if (env.IsEnvironment("AppDevelopment"))
            {
                // steps to debug angular app along with api
                app.UseSpa(spa =>
                {
                    if (!IsPortOccupied(4200))
                    {
                        spa.Options.SourcePath = "app";
                        spa.UseAngularCliServer(npmScript: "start");
                    }

                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                });
            }
        }

        private static bool IsPortOccupied(int port)
        {
            var availablePorts = new List<int>();

            var properties = IPGlobalProperties.GetIPGlobalProperties();

            // Active connections
            var connections = properties.GetActiveTcpConnections();
            availablePorts.AddRange(connections.Select(tcpConn => tcpConn.LocalEndPoint.Port));

            var endPointsTcp = properties.GetActiveTcpListeners();
            availablePorts.AddRange(endPointsTcp.Select(tcp => tcp.Port));

            // Active udp listeners
            var endPointsUdp = properties.GetActiveUdpListeners();
            availablePorts.AddRange(endPointsUdp.Select(udp => udp.Port));

            return availablePorts.Any(p => p == port);
        }
    }
}