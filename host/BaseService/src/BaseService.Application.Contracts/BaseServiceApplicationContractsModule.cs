using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using SightsService;
using CommonService;
using StorageService;

namespace BaseService;

[DependsOn(
    typeof(BaseServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(SightsServiceApplicationContractsModule),
    typeof(CommonServiceApplicationContractsModule),
    typeof(StorageServiceApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class BaseServiceApplicationContractsModule : AbpModule
{

}
