using CommonService.Localization;
using Volo.Abp.Application.Services;

namespace CommonService;

public abstract class CommonServiceAppService : ApplicationService
{
    protected CommonServiceAppService()
    {
        LocalizationResource = typeof(CommonServiceResource);
        ObjectMapperContext = typeof(CommonServiceApplicationModule);
    }
}
