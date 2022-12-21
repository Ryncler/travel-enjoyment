using Volo.Abp.Modularity;

namespace BaseService;

[DependsOn(
    typeof(BaseServiceApplicationModule),
    typeof(BaseServiceDomainTestModule)
    )]
public class BaseServiceApplicationTestModule : AbpModule
{

}
