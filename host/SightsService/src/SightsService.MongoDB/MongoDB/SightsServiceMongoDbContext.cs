using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace SightsService.MongoDB;

[ConnectionStringName(SightsServiceDbProperties.ConnectionStringName)]
public class SightsServiceMongoDbContext : AbpMongoDbContext, ISightsServiceMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureSightsService();
    }
}
