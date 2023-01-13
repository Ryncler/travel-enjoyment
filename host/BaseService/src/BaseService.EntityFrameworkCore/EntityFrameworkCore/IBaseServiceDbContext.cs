using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using BaseService.Entities;

namespace BaseService.EntityFrameworkCore;

[ConnectionStringName(BaseServiceDbProperties.ConnectionStringName)]
public interface IBaseServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    DbSet<UserExtension> UserExtensions { get; set; }
    DbSet<EntryInfo> EntryInfos { get; set; }
}
