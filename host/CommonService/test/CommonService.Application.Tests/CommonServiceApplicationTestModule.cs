using Volo.Abp.Modularity;

namespace CommonService;

[DependsOn(
    typeof(CommonServiceApplicationModule),
    typeof(CommonServiceDomainTestModule)
    )]
public class CommonServiceApplicationTestModule : AbpModule
{

}
