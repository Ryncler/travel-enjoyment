using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CommonService.EntityFrameworkCore;

public class CommonServiceHttpApiHostMigrationsDbContext : AbpDbContext<CommonServiceHttpApiHostMigrationsDbContext>
{
    public CommonServiceHttpApiHostMigrationsDbContext(DbContextOptions<CommonServiceHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureCommonService();
    }
}
