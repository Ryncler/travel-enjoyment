using SightsService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace SightsService.Permissions;

public class SightsServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SightsServicePermissions.GroupName, L("景点服务管理"));

        var sightsPermission = myGroup.AddPermission(SightsServicePermissions.Sights.Default, L("景点管理"));
        sightsPermission.AddChild(SightsServicePermissions.Sights.Create, L("创建"));
        sightsPermission.AddChild(SightsServicePermissions.Sights.Update, L("更新"));
        sightsPermission.AddChild(SightsServicePermissions.Sights.Delete, L("删除"));

        var travelsPermission = myGroup.AddPermission(SightsServicePermissions.Travels.Default, L("游记管理"));
        travelsPermission.AddChild(SightsServicePermissions.Travels.Create, L("创建"));
        travelsPermission.AddChild(SightsServicePermissions.Travels.Update, L("更新"));
        travelsPermission.AddChild(SightsServicePermissions.Travels.Delete, L("删除"));

        var activityPermission = myGroup.AddPermission(SightsServicePermissions.Activity.Default, L("活动管理"));
        activityPermission.AddChild(SightsServicePermissions.Activity.Create, L("创建"));
        activityPermission.AddChild(SightsServicePermissions.Activity.Update, L("更新"));
        activityPermission.AddChild(SightsServicePermissions.Activity.Delete, L("删除"));

        var commentPermission = myGroup.AddPermission(SightsServicePermissions.Comment.Default, L("评论管理"));
        commentPermission.AddChild(SightsServicePermissions.Comment.Create, L("创建"));
        commentPermission.AddChild(SightsServicePermissions.Comment.Update, L("更新"));
        commentPermission.AddChild(SightsServicePermissions.Comment.Delete, L("删除"));

        var sightsTravelsPermission = myGroup.AddPermission(SightsServicePermissions.SightsTravels.Default, L("景点游记管理"));
        sightsTravelsPermission.AddChild(SightsServicePermissions.SightsTravels.Create, L("创建"));
        sightsTravelsPermission.AddChild(SightsServicePermissions.SightsTravels.Update, L("更新"));
        sightsTravelsPermission.AddChild(SightsServicePermissions.SightsTravels.Delete, L("删除"));

        var sightsActivityPermission = myGroup.AddPermission(SightsServicePermissions.SightsActivity.Default, L("景点活动管理"));
        sightsActivityPermission.AddChild(SightsServicePermissions.SightsActivity.Create, L("创建"));
        sightsActivityPermission.AddChild(SightsServicePermissions.SightsActivity.Update, L("更新"));
        sightsActivityPermission.AddChild(SightsServicePermissions.SightsActivity.Delete, L("删除"));

        var travelsExtentionPermission = myGroup.AddPermission(SightsServicePermissions.TravelsExtention.Default, L("游记扩展管理"));
        travelsExtentionPermission.AddChild(SightsServicePermissions.TravelsExtention.Create, L("创建"));
        travelsExtentionPermission.AddChild(SightsServicePermissions.TravelsExtention.Update, L("更新"));
        travelsExtentionPermission.AddChild(SightsServicePermissions.TravelsExtention.Delete, L("删除"));

        var sightsTagPermission = myGroup.AddPermission(SightsServicePermissions.SightsTag.Default, L("景点标签管理"));
        sightsTagPermission.AddChild(SightsServicePermissions.SightsTag.Create, L("创建"));
        sightsTagPermission.AddChild(SightsServicePermissions.SightsTag.Update, L("更新"));
        sightsTagPermission.AddChild(SightsServicePermissions.SightsTag.Delete, L("删除"));

        var userChoiceTravelsPermission = myGroup.AddPermission(SightsServicePermissions.UserChoiceTravels.Default, L("用户精选游记管理"));
        userChoiceTravelsPermission.AddChild(SightsServicePermissions.UserChoiceTravels.Create, L("创建"));
        userChoiceTravelsPermission.AddChild(SightsServicePermissions.UserChoiceTravels.Update, L("更新"));
        userChoiceTravelsPermission.AddChild(SightsServicePermissions.UserChoiceTravels.Delete, L("删除"));

        var userTrendsPermission = myGroup.AddPermission(SightsServicePermissions.UserTrends.Default, L("用户动态管理"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SightsServiceResource>(name);
    }
}
