using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace SightsService;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class SightsServiceInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SightsServiceInstallerModule>();
        });
    }
}
