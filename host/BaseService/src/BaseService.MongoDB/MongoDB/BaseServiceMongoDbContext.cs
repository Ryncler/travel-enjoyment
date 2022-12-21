using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace BaseService.MongoDB;

[ConnectionStringName(BaseServiceDbProperties.ConnectionStringName)]
public class BaseServiceMongoDbContext : AbpMongoDbContext, IBaseServiceMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureBaseService();
    }
}
