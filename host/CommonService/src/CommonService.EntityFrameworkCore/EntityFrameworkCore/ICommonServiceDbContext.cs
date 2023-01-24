using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using CommonService.GeoManage;
using CommonService.CategoryManage;

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
}
