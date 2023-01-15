using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace StorageService.MongoDB;

[ConnectionStringName(StorageServiceDbProperties.ConnectionStringName)]
public interface IStorageServiceMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
