using Localization.Resources.AbpUi;
using StorageService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace StorageService;

[DependsOn(
    typeof(StorageServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class StorageServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(StorageServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<StorageServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
