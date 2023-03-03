using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using CommonService.GeoManage;
using CommonService.CategoryManage;
using CommonService.Top;
using CommonService.User;

namespace CommonService.EntityFrameworkCore;

[ConnectionStringName(CommonServiceDbProperties.ConnectionStringName)]
public interface ICommonServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    DbSet<GeoInfo> GeoInfos { get; set; }
    DbSet<Tag> Tags { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<HotTop> HotTops { get; set; }
    DbSet<UserTravel> UserTravels { get; set; }
}
