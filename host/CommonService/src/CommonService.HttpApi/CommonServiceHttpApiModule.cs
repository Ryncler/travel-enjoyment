using Localization.Resources.AbpUi;
using CommonService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace CommonService;

[DependsOn(
    typeof(CommonServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class CommonServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CommonServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<CommonServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
