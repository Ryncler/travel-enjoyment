using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace BaseService;

[DependsOn(
    typeof(BaseServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class BaseServiceApplicationContractsModule : AbpModule
{

}
