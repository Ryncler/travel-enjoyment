using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace SightsService.MongoDB;

[ConnectionStringName(SightsServiceDbProperties.ConnectionStringName)]
public interface ISightsServiceMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
