using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace BaseService;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BaseServiceHttpApiClientModule)
    )]
public class BaseServiceConsoleApiClientModule : AbpModule
{

}
