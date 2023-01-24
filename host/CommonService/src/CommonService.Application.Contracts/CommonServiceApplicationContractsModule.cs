using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace CommonService;

[DependsOn(
    typeof(CommonServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class CommonServiceApplicationContractsModule : AbpModule
{

}
