using BaseService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace BaseService.Permissions;

public class BaseServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BaseServicePermissions.GroupName, L("�����������"));

        var userExtensionPermission = myGroup.AddPermission(BaseServicePermissions.UserExtension.Default, L("�û���չ����"));
        userExtensionPermission.AddChild(BaseServicePermissions.UserExtension.Create, L("����"));
        userExtensionPermission.AddChild(BaseServicePermissions.UserExtension.Update, L("����"));
        userExtensionPermission.AddChild(BaseServicePermissions.UserExtension.Delete, L("ɾ��"));

        var openIddictPermission = myGroup.AddPermission(BaseServicePermissions.OpenIddict.Default, L("OpenIddict����"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.CreateApplication, L("����Ӧ��"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.UpdateApplication, L("����Ӧ��"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.DeleteApplication, L("ɾ��Ӧ��"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.CreateScope, L("��������"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.UpdateScope, L("���·���"));
        openIddictPermission.AddChild(BaseServicePermissions.OpenIddict.DeleteScope, L("ɾ������"));

        var userManage = myGroup.AddPermission(BaseServicePermissions.UserManage.Default, L("�û�����"));
        userManage.AddChild(BaseServicePermissions.UserManage.Create, L("����"));
        userManage.AddChild(BaseServicePermissions.UserManage.Update, L("����"));
        userManage.AddChild(BaseServicePermissions.UserManage.Delete, L("ɾ��"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BaseServiceResource>(name);
    }
}
