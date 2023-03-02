using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using StorageService.Storage;

namespace StorageService.EntityFrameworkCore;

[ConnectionStringName(StorageServiceDbProperties.ConnectionStringName)]
public interface IStorageServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    DbSet<Image> Images { get; set; }
}
