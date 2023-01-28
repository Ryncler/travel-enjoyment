using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using SightsService;

namespace BaseService;

[DependsOn(
    typeof(BaseServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(SightsServiceApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class BaseServiceApplicationContractsModule : AbpModule
{

}
