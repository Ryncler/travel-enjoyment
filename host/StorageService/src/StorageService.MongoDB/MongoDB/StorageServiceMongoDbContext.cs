using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace StorageService.MongoDB;

[ConnectionStringName(StorageServiceDbProperties.ConnectionStringName)]
public class StorageServiceMongoDbContext : AbpMongoDbContext, IStorageServiceMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureStorageService();
    }
}
