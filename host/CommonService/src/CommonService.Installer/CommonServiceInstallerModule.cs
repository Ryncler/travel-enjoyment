using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace CommonService;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class CommonServiceInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CommonServiceInstallerModule>();
        });
    }
}
