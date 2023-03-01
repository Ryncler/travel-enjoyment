using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using SightsService.SightsManage;
using SightsService.TravelsManage;
using SightsService.ActivityManage;
using SightsService.CommentManage;

namespace SightsService.EntityFrameworkCore;

[ConnectionStringName(SightsServiceDbProperties.ConnectionStringName)]
public interface ISightsServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    DbSet<Sights> Sights { get; set; }
    DbSet<Travels> Travels { get; set; }
    DbSet<Activity> Activities { get; set; }
    DbSet<Comment> Comments { get; set; }
    DbSet<SightsTravels> SightsTravels { get; set; }
    DbSet<SightsActivity> SightsActivities { get; set; }
    DbSet<TravelsExtention> TravelsExtentions { get; set; }
    DbSet<SightsTag> SightsTags { get; set; }
}
