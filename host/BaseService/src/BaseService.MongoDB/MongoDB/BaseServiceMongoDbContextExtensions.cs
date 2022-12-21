using Volo.Abp;
using Volo.Abp.MongoDB;

namespace BaseService.MongoDB;

public static class BaseServiceMongoDbContextExtensions
{
    public static void ConfigureBaseService(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
