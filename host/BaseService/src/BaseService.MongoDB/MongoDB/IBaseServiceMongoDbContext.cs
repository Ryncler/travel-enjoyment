using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace BaseService.MongoDB;

[ConnectionStringName(BaseServiceDbProperties.ConnectionStringName)]
public interface IBaseServiceMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
