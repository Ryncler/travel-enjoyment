using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace SightsService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(SightsServiceDomainSharedModule)
)]
public class SightsServiceDomainModule : AbpModule
{

}
