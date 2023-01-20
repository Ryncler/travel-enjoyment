using Volo.Abp.Modularity;

namespace SightsService;

[DependsOn(
    typeof(SightsServiceApplicationModule),
    typeof(SightsServiceDomainTestModule)
    )]
public class SightsServiceApplicationTestModule : AbpModule
{

}
