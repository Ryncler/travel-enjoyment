using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using BaseService.Entities;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace BaseService.EntityFrameworkCore;

[ConnectionStringName(BaseServiceDbProperties.ConnectionStringName)]
public class BaseServiceDbContext : AbpDbContext<BaseServiceDbContext>, IBaseServiceDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DbSet<UserExtension> UserExtensions { get; set; }
    public DbSet<EntryInfo> EntryInfos { get; set; }

    public BaseServiceDbContext(DbContextOptions<BaseServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureBaseService();
    }
}
