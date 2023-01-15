using Volo.Abp.Modularity;

namespace StorageService;

[DependsOn(
    typeof(StorageServiceApplicationModule),
    typeof(StorageServiceDomainTestModule)
    )]
public class StorageServiceApplicationTestModule : AbpModule
{

}
