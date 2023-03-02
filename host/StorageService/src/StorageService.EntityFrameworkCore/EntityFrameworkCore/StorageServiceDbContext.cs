using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using StorageService.Storage;

namespace StorageService.EntityFrameworkCore;

[ConnectionStringName(StorageServiceDbProperties.ConnectionStringName)]
public class StorageServiceDbContext : AbpDbContext<StorageServiceDbContext>, IStorageServiceDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<Image> Images { get; set; }

    public StorageServiceDbContext(DbContextOptions<StorageServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureStorageService();
    }
}
