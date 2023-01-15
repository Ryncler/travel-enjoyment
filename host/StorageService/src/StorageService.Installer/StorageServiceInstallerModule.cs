using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace StorageService;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class StorageServiceInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<StorageServiceInstallerModule>();
        });
    }
}
