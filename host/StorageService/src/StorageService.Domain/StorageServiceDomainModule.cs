using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace StorageService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(StorageServiceDomainSharedModule)
)]
public class StorageServiceDomainModule : AbpModule
{

}
