using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace SightsService;

[DependsOn(
    typeof(SightsServiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class SightsServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(SightsServiceApplicationContractsModule).Assembly,
            SightsServiceRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SightsServiceHttpApiClientModule>();
        });

    }
}
