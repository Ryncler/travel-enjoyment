using SightsService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace SightsService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(SightsServiceEntityFrameworkCoreTestModule)
    )]
public class SightsServiceDomainTestModule : AbpModule
{

}
