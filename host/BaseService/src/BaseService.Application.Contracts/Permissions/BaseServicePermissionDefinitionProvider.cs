using BaseService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace BaseService.Permissions;

public class BaseServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BaseServicePermissions.GroupName, L("基础服务管理"));

        var userExtensionPermission = myGroup.AddPermission(BaseServicePermissions.UserExtension.Default, L("用户扩展管理"));
        userExtensionPermission.AddChild(BaseServicePermissions.UserExtension.Create, L("创建"));
        userExtensionPermission.AddChild(BaseServicePermissions.UserExtension.Update, L("更新"));
        userExtensionPermission.AddChild(BaseServicePermissions.UserExtension.Delete, L("删除"));

        var openIddictPermission = myGroup.AddPermission(BaseServicePermissions.OpenIddict.Default, L("OpenIddict管理"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.CreateApplication, L("创建应用"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.UpdateApplication, L("更新应用"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.DeleteApplication, L("删除应用"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.CreateScope, L("创建服务"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.UpdateScope, L("更新服务"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.DeleteScope, L("删除服务"));

        var userManage = myGroup.AddPermission(BaseServicePermissions.UserManage.Default, L("用户管理"));
        userManage.AddChild(BaseServicePermissions.UserManage.Create, L("创建"));
        userManage.AddChild(BaseServicePermissions.UserManage.Update, L("更新"));
        userManage.AddChild(BaseServicePermissions.UserManage.Delete, L("删除"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BaseServiceResource>(name);
    }
}
