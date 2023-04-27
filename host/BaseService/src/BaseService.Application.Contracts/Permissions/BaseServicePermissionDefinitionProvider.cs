using BaseService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace BaseService.Permissions;

public class BaseServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BaseServicePermissions.GroupName, L("基础服务管理"));

        var openIddictPermission = myGroup.AddPermission(BaseServicePermissions.OpenIddict.Default, L("OpenIddict管理"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.CreateApplication, L("创建"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.UpdateApplication, L("更新"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.DeleteApplication, L("删除"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.CreateScope, L("创建"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.UpdateScope, L("更新"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.DeleteScope, L("删除"));

        var userManage = myGroup.AddPermission(BaseServicePermissions.UserManage.Default, L("用户管理"));
        userManage.AddChild(BaseServicePermissions.UserManage.Create, L("创建"));
        userManage.AddChild(BaseServicePermissions.UserManage.Update, L("更新"));
        userManage.AddChild(BaseServicePermissions.UserManage.Delete, L("删除"));

        var entryInfoPermission = myGroup.AddPermission(BaseServicePermissions.EntryInfo.Default, L("入驻管理"));
        entryInfoPermission.AddChild(BaseServicePermissions.EntryInfo.Create, L("创建"));
        entryInfoPermission.AddChild(BaseServicePermissions.EntryInfo.Update, L("更新"));
        entryInfoPermission.AddChild(BaseServicePermissions.EntryInfo.Delete, L("删除"));

        var auditLog = myGroup.AddPermission(BaseServicePermissions.AuditLog.Default, L("日志管理"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BaseServiceResource>(name);
    }
}
