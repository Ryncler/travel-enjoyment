using Volo.Abp;
using Volo.Abp.MongoDB;

namespace CommonService.MongoDB;

public static class CommonServiceMongoDbContextExtensions
{
    public static void ConfigureCommonService(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
