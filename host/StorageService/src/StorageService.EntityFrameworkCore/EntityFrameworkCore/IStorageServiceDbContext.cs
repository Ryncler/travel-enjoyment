using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace StorageService.EntityFrameworkCore;

[ConnectionStringName(StorageServiceDbProperties.ConnectionStringName)]
public interface IStorageServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
