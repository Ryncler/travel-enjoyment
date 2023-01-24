using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace CommonService;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CommonServiceHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class CommonServiceConsoleApiClientModule : AbpModule
{

}
