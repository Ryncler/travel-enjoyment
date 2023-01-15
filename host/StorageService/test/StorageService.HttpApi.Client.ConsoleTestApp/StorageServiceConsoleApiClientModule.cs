using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace StorageService;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(StorageServiceHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class StorageServiceConsoleApiClientModule : AbpModule
{

}
