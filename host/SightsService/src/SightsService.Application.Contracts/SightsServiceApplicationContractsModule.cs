using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace SightsService;

[DependsOn(
    typeof(SightsServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class SightsServiceApplicationContractsModule : AbpModule
{

}
