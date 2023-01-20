using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using SightsService.SightsManage;
using SightsService.TravelsManage;
using SightsService.ActivityManage;
using SightsService.CommentManage;

namespace SightsService.EntityFrameworkCore;

[ConnectionStringName(SightsServiceDbProperties.ConnectionStringName)]
public class SightsServiceDbContext : AbpDbContext<SightsServiceDbContext>, ISightsServiceDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<Sights> Sights { get; set; }
    public DbSet<Travels> Travels { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<SightsTravels> SightsTravels { get; set; }
    public DbSet<SightsActivity> SightsActivities { get; set; }
    public DbSet<TravelsExtention> TravelsExtentions { get; set; }

    public SightsServiceDbContext(DbContextOptions<SightsServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureSightsService();
    }
}
