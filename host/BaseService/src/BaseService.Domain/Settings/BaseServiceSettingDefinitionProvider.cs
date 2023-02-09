using Volo.Abp.Emailing;
using Volo.Abp.Settings;

namespace BaseService.Settings;

public class BaseServiceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        /* Define module settings here.
         * Use names from BaseServiceSettings class.
         */

        var langurage = context.GetOrNull("Abp.Localization.DefaultLanguage");
        if (langurage != null)
            langurage.DefaultValue = "zh-Hans";

        var time = context.GetOrNull("Abp.Timing.TimeZone");
        if (time != null)
            time.DefaultValue = "China Standard Time";

    }
}
