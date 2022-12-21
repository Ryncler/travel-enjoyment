using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace BaseService.EntityFrameworkCore;

[ConnectionStringName(BaseServiceDbProperties.ConnectionStringName)]
public interface IBaseServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
