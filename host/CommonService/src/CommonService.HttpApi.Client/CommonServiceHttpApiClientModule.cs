using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace CommonService;

[DependsOn(
    typeof(CommonServiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class CommonServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(CommonServiceApplicationContractsModule).Assembly,
            CommonServiceRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CommonServiceHttpApiClientModule>();
        });

    }
}
