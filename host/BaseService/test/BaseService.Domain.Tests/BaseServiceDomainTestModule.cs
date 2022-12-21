using BaseService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace BaseService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(BaseServiceEntityFrameworkCoreTestModule)
    )]
public class BaseServiceDomainTestModule : AbpModule
{

}
