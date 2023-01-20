using Volo.Abp;
using Volo.Abp.MongoDB;

namespace SightsService.MongoDB;

public static class SightsServiceMongoDbContextExtensions
{
    public static void ConfigureSightsService(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
