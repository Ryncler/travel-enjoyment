using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace CommonService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(CommonServiceDomainSharedModule)
)]
public class CommonServiceDomainModule : AbpModule
{

}
