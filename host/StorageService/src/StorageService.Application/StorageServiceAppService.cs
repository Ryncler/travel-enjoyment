using StorageService.Localization;
using Volo.Abp.Application.Services;

namespace StorageService;

public abstract class StorageServiceAppService : ApplicationService
{
    protected StorageServiceAppService()
    {
        LocalizationResource = typeof(StorageServiceResource);
        ObjectMapperContext = typeof(StorageServiceApplicationModule);
    }
}
