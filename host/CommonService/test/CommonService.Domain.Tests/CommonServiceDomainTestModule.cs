using CommonService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CommonService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(CommonServiceEntityFrameworkCoreTestModule)
    )]
public class CommonServiceDomainTestModule : AbpModule
{

}
