using SightsService.Localization;
using Volo.Abp.Application.Services;

namespace SightsService;

public abstract class SightsServiceAppService : ApplicationService
{
    protected SightsServiceAppService()
    {
        LocalizationResource = typeof(SightsServiceResource);
        ObjectMapperContext = typeof(SightsServiceApplicationModule);
    }
}
