using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace SightsService;

[DependsOn(
    typeof(SightsServiceDomainModule),
    typeof(SightsServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class SightsServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<SightsServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<SightsServiceApplicationModule>(validate: true);
        });
    }
}
