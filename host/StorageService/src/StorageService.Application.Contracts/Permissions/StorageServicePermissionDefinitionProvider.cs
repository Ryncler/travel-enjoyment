using StorageService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace StorageService.Permissions;

public class StorageServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(StorageServicePermissions.GroupName, L("Permission:StorageService"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<StorageServiceResource>(name);
    }
}
