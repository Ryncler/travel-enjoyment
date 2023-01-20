using SightsService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace SightsService.Permissions;

public class SightsServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SightsServicePermissions.GroupName, L("Permission:SightsService"));

        var sightsPermission = myGroup.AddPermission(SightsServicePermissions.Sights.Default, L("Permission:Sights"));
        sightsPermission.AddChild(SightsServicePermissions.Sights.Create, L("Permission:Create"));
        sightsPermission.AddChild(SightsServicePermissions.Sights.Update, L("Permission:Update"));
        sightsPermission.AddChild(SightsServicePermissions.Sights.Delete, L("Permission:Delete"));

        var travelsPermission = myGroup.AddPermission(SightsServicePermissions.Travels.Default, L("Permission:Travels"));
        travelsPermission.AddChild(SightsServicePermissions.Travels.Create, L("Permission:Create"));
        travelsPermission.AddChild(SightsServicePermissions.Travels.Update, L("Permission:Update"));
        travelsPermission.AddChild(SightsServicePermissions.Travels.Delete, L("Permission:Delete"));

        var activityPermission = myGroup.AddPermission(SightsServicePermissions.Activity.Default, L("Permission:Activity"));
        activityPermission.AddChild(SightsServicePermissions.Activity.Create, L("Permission:Create"));
        activityPermission.AddChild(SightsServicePermissions.Activity.Update, L("Permission:Update"));
        activityPermission.AddChild(SightsServicePermissions.Activity.Delete, L("Permission:Delete"));

        var commentPermission = myGroup.AddPermission(SightsServicePermissions.Comment.Default, L("Permission:Comment"));
        commentPermission.AddChild(SightsServicePermissions.Comment.Create, L("Permission:Create"));
        commentPermission.AddChild(SightsServicePermissions.Comment.Update, L("Permission:Update"));
        commentPermission.AddChild(SightsServicePermissions.Comment.Delete, L("Permission:Delete"));

        var sightsTravelsPermission = myGroup.AddPermission(SightsServicePermissions.SightsTravels.Default, L("Permission:SightsTravels"));
        sightsTravelsPermission.AddChild(SightsServicePermissions.SightsTravels.Create, L("Permission:Create"));
        sightsTravelsPermission.AddChild(SightsServicePermissions.SightsTravels.Update, L("Permission:Update"));
        sightsTravelsPermission.AddChild(SightsServicePermissions.SightsTravels.Delete, L("Permission:Delete"));

        var sightsActivityPermission = myGroup.AddPermission(SightsServicePermissions.SightsActivity.Default, L("Permission:SightsActivity"));
        sightsActivityPermission.AddChild(SightsServicePermissions.SightsActivity.Create, L("Permission:Create"));
        sightsActivityPermission.AddChild(SightsServicePermissions.SightsActivity.Update, L("Permission:Update"));
        sightsActivityPermission.AddChild(SightsServicePermissions.SightsActivity.Delete, L("Permission:Delete"));

        var travelsExtentionPermission = myGroup.AddPermission(SightsServicePermissions.TravelsExtention.Default, L("Permission:TravelsExtention"));
        travelsExtentionPermission.AddChild(SightsServicePermissions.TravelsExtention.Create, L("Permission:Create"));
        travelsExtentionPermission.AddChild(SightsServicePermissions.TravelsExtention.Update, L("Permission:Update"));
        travelsExtentionPermission.AddChild(SightsServicePermissions.TravelsExtention.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SightsServiceResource>(name);
    }
}
