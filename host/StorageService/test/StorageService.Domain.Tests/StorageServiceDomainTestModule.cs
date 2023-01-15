using StorageService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace StorageService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(StorageServiceEntityFrameworkCoreTestModule)
    )]
public class StorageServiceDomainTestModule : AbpModule
{

}
