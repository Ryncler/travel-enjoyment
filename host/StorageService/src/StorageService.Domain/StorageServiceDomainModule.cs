using Microsoft.Extensions.DependencyInjection;
using StorageService.Minio;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace StorageService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(StorageServiceDomainSharedModule)
)]
public class StorageServiceDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpMinioOptions>(options =>
        {
            options.EndPoint = configuration["Minio:IP"];
            options.AccessKey = configuration["Minio:AccessKey"];
            options.SecretKey = configuration["Minio:SecretKey"];
            options.Region = configuration["Minio:Region"];
            //options.TransientMinioProvider(context);
        });
    }
}
