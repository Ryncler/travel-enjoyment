using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace StorageService.EntityFrameworkCore;

public class StorageServiceHttpApiHostMigrationsDbContext : AbpDbContext<StorageServiceHttpApiHostMigrationsDbContext>
{
    public StorageServiceHttpApiHostMigrationsDbContext(DbContextOptions<StorageServiceHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureStorageService();
    }
}
