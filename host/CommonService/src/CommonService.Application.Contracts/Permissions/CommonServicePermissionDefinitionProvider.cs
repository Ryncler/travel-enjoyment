using CommonService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CommonService.Permissions;

public class CommonServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CommonServicePermissions.GroupName, L("公共服务管理"));

        var geoInfoPermission = myGroup.AddPermission(CommonServicePermissions.GeoInfo.Default, L("地理信息管理"));
        geoInfoPermission.AddChild(CommonServicePermissions.GeoInfo.Create, L("创建"));
        geoInfoPermission.AddChild(CommonServicePermissions.GeoInfo.Update, L("更新"));
        geoInfoPermission.AddChild(CommonServicePermissions.GeoInfo.Delete, L("删除"));

        var tagPermission = myGroup.AddPermission(CommonServicePermissions.Tag.Default, L("标签管理"));
        tagPermission.AddChild(CommonServicePermissions.Tag.Create, L("创建"));
        tagPermission.AddChild(CommonServicePermissions.Tag.Update, L("更新"));
        tagPermission.AddChild(CommonServicePermissions.Tag.Delete, L("删除"));

        var categoryPermission = myGroup.AddPermission(CommonServicePermissions.Category.Default, L("类别管理"));
        categoryPermission.AddChild(CommonServicePermissions.Category.Create, L("创建"));
        categoryPermission.AddChild(CommonServicePermissions.Category.Update, L("更新"));
        categoryPermission.AddChild(CommonServicePermissions.Category.Delete, L("删除"));

        var userTravelPermission = myGroup.AddPermission(CommonServicePermissions.UserTravel.Default, L("用户收藏管理"));
        userTravelPermission.AddChild(CommonServicePermissions.UserTravel.Create, L("创建"));
        userTravelPermission.AddChild(CommonServicePermissions.UserTravel.Update, L("更新"));
        userTravelPermission.AddChild(CommonServicePermissions.UserTravel.Delete, L("收藏"));

        var hotTopPermission = myGroup.AddPermission(CommonServicePermissions.HotTop.Default, L("热门管理"));
        hotTopPermission.AddChild(CommonServicePermissions.HotTop.Create, L("创建"));
        hotTopPermission.AddChild(CommonServicePermissions.HotTop.Update, L("更新"));
        hotTopPermission.AddChild(CommonServicePermissions.HotTop.Delete, L("收藏"));

        var dashboardPermission = myGroup.AddPermission(CommonServicePermissions.Dashboard.Default, L("仪表盘管理"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CommonServiceResource>(name);
    }
}
