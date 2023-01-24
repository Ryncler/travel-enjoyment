using CommonService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CommonService.Permissions;

public class CommonServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CommonServicePermissions.GroupName, L("Permission:CommonService"));

        var geoInfoPermission = myGroup.AddPermission(CommonServicePermissions.GeoInfo.Default, L("Permission:GeoInfo"));
        geoInfoPermission.AddChild(CommonServicePermissions.GeoInfo.Create, L("Permission:Create"));
        geoInfoPermission.AddChild(CommonServicePermissions.GeoInfo.Update, L("Permission:Update"));
        geoInfoPermission.AddChild(CommonServicePermissions.GeoInfo.Delete, L("Permission:Delete"));

        var tagPermission = myGroup.AddPermission(CommonServicePermissions.Tag.Default, L("Permission:Tag"));
        tagPermission.AddChild(CommonServicePermissions.Tag.Create, L("Permission:Create"));
        tagPermission.AddChild(CommonServicePermissions.Tag.Update, L("Permission:Update"));
        tagPermission.AddChild(CommonServicePermissions.Tag.Delete, L("Permission:Delete"));

        var categoryPermission = myGroup.AddPermission(CommonServicePermissions.Category.Default, L("Permission:Category"));
        categoryPermission.AddChild(CommonServicePermissions.Category.Create, L("Permission:Create"));
        categoryPermission.AddChild(CommonServicePermissions.Category.Update, L("Permission:Update"));
        categoryPermission.AddChild(CommonServicePermissions.Category.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CommonServiceResource>(name);
    }
}
