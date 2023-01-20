using SightsService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace SightsService;

public abstract class SightsServiceController : AbpControllerBase
{
    protected SightsServiceController()
    {
        LocalizationResource = typeof(SightsServiceResource);
    }
}
