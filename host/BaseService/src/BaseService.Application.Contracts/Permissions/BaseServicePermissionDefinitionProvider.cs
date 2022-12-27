using BaseService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace BaseService.Permissions;

public class BaseServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BaseServicePermissions.GroupName, L("Permission:BaseService"));

        var userExtensionPermission = myGroup.AddPermission(BaseServicePermissions.UserExtension.Default, L("Permission:UserExtension"));
        userExtensionPermission.AddChild(BaseServicePermissions.UserExtension.Create, L("Permission:Create"));
        userExtensionPermission.AddChild(BaseServicePermissions.UserExtension.Update, L("Permission:Update"));
        userExtensionPermission.AddChild(BaseServicePermissions.UserExtension.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BaseServiceResource>(name);
    }
}
