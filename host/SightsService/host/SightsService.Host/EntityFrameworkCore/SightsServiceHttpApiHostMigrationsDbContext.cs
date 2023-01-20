using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SightsService.EntityFrameworkCore;

public class SightsServiceHttpApiHostMigrationsDbContext : AbpDbContext<SightsServiceHttpApiHostMigrationsDbContext>
{
    public SightsServiceHttpApiHostMigrationsDbContext(DbContextOptions<SightsServiceHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureSightsService();
    }
}
