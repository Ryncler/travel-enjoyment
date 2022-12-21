using BaseService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace BaseService;

public abstract class BaseServiceController : AbpControllerBase
{
    protected BaseServiceController()
    {
        LocalizationResource = typeof(BaseServiceResource);
    }
}
