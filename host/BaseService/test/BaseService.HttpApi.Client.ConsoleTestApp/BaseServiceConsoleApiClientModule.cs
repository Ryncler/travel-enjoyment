using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace BaseService;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BaseServiceHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class BaseServiceConsoleApiClientModule : AbpModule
{

}
