using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace StorageService;

[DependsOn(
    typeof(StorageServiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class StorageServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(StorageServiceApplicationContractsModule).Assembly,
            StorageServiceRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<StorageServiceHttpApiClientModule>();
        });

    }
}
