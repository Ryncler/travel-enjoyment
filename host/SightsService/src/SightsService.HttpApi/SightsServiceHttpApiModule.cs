using Localization.Resources.AbpUi;
using SightsService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace SightsService;

[DependsOn(
    typeof(SightsServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class SightsServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SightsServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SightsServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
