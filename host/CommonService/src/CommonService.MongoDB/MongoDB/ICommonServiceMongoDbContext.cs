using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace CommonService.MongoDB;

[ConnectionStringName(CommonServiceDbProperties.ConnectionStringName)]
public interface ICommonServiceMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
