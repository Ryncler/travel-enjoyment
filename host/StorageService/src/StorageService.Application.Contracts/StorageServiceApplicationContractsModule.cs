using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace StorageService;

[DependsOn(
    typeof(StorageServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class StorageServiceApplicationContractsModule : AbpModule
{

}
