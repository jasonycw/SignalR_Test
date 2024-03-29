﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalR_Server.Hub;

namespace SignalR_Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSignalR(options => options.EnableDetailedErrors = true)
                //.AddRedis() // For syncing clients across servers
                ;
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:11895") // This is for cross-domain connections
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
            app.UseSignalR(route =>
            {
                route.MapHub<SomeHub>($"/{nameof(SomeHub)}");// This is the hub URL
                route.MapHub<OtherHub>($"/{nameof(OtherHub)}");
            });
            app.UseMvc();
        }
    }
}
