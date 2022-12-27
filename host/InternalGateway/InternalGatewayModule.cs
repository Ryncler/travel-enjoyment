using BaseService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace InternalGateway
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(BaseServiceHttpApiModule),
        typeof(AbpAspNetCoreSerilogModule)
    )]
    public class InternalGatewayModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureSwaggerServices(context);
            context.Services.AddOcelot(context.Services.GetConfiguration());
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseCorrelationId();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var configuration = context.GetConfiguration();
                var routes = configuration.GetSection("Routes").Get<List<OcelotConfiguration>>();
                var routedServices = routes
                    .GroupBy(t => t.ServiceKey)
                    .Select(r => r.First())
                    .Distinct();
                foreach (var config in routedServices.OrderBy(q => q.ServiceKey))
                {
                    var url = $"{config.DownstreamScheme}://{config.DownstreamHostAndPorts.FirstOrDefault()?.Host}:{config.DownstreamHostAndPorts.FirstOrDefault()?.Port}";
                    if (!env.IsDevelopment())
                    {
                        url = $"https://{config.DownstreamHostAndPorts.FirstOrDefault()?.Host}";
                    }

                    options.SwaggerEndpoint($"{url}/swagger/v1/swagger.json", $"{config.ServiceKey} API");
                    options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                    options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
                }
            });
            app.UseOcelot().Wait();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "InternalGateway Service API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                });
        }
    }
}
