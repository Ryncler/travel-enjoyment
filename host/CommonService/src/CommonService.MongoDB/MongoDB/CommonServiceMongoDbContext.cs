using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace CommonService.MongoDB;

[ConnectionStringName(CommonServiceDbProperties.ConnectionStringName)]
public class CommonServiceMongoDbContext : AbpMongoDbContext, ICommonServiceMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureCommonService();
    }
}
