using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using CommonService.GeoManage;
using CommonService.CategoryManage;

namespace CommonService.EntityFrameworkCore;

[ConnectionStringName(CommonServiceDbProperties.ConnectionStringName)]
public class CommonServiceDbContext : AbpDbContext<CommonServiceDbContext>, ICommonServiceDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<GeoInfo> GeoInfos { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }

    public CommonServiceDbContext(DbContextOptions<CommonServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureCommonService();
    }
}
