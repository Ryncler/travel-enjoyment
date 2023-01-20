using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace SightsService;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SightsServiceHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class SightsServiceConsoleApiClientModule : AbpModule
{

}
