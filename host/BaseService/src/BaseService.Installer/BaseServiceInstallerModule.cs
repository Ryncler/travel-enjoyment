using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace BaseService;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class BaseServiceInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BaseServiceInstallerModule>();
        });
    }
}
