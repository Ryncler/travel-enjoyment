using BaseService.Localization;
using Volo.Abp.Application.Services;

namespace BaseService;

public abstract class BaseServiceAppService : ApplicationService
{
    protected BaseServiceAppService()
    {
        LocalizationResource = typeof(BaseServiceResource);
        ObjectMapperContext = typeof(BaseServiceApplicationModule);
    }
}
