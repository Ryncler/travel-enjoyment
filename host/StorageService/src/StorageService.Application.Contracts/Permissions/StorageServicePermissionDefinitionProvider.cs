using StorageService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace StorageService.Permissions;

public class StorageServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(StorageServicePermissions.GroupName, L("Permission:StorageService"));

        var imagePermission = myGroup.AddPermission(StorageServicePermissions.Image.Default, L("Permission:Image"));
        imagePermission.AddChild(StorageServicePermissions.Image.Create, L("Permission:Create"));
        imagePermission.AddChild(StorageServicePermissions.Image.Update, L("Permission:Update"));
        imagePermission.AddChild(StorageServicePermissions.Image.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<StorageServiceResource>(name);
    }
}
