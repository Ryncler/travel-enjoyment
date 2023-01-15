using Volo.Abp;
using Volo.Abp.MongoDB;

namespace StorageService.MongoDB;

public static class StorageServiceMongoDbContextExtensions
{
    public static void ConfigureStorageService(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
