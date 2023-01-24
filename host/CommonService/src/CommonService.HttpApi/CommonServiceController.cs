using CommonService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CommonService;

public abstract class CommonServiceController : AbpControllerBase
{
    protected CommonServiceController()
    {
        LocalizationResource = typeof(CommonServiceResource);
    }
}
