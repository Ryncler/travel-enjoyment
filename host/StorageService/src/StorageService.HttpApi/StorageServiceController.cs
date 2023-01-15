using StorageService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace StorageService;

public abstract class StorageServiceController : AbpControllerBase
{
    protected StorageServiceController()
    {
        LocalizationResource = typeof(StorageServiceResource);
    }
}
