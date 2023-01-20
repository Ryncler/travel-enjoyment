using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace SightsService.MongoDB;

[DependsOn(
    typeof(SightsServiceDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class SightsServiceMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<SightsServiceMongoDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
        });
    }
}
