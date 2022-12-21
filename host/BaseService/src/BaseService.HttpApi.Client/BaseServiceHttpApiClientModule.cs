using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace BaseService;

[DependsOn(
    typeof(BaseServiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class BaseServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(BaseServiceApplicationContractsModule).Assembly,
            BaseServiceRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BaseServiceHttpApiClientModule>();
        });

    }
}
