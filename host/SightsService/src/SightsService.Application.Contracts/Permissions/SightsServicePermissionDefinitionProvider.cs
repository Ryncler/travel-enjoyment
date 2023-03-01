using SightsService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace SightsService.Permissions;

public class SightsServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SightsServicePermissions.GroupName, L("����������"));

        var sightsPermission = myGroup.AddPermission(SightsServicePermissions.Sights.Default, L("�������"));
        sightsPermission.AddChild(SightsServicePermissions.Sights.Create, L("����"));
        sightsPermission.AddChild(SightsServicePermissions.Sights.Update, L("����"));
        sightsPermission.AddChild(SightsServicePermissions.Sights.Delete, L("ɾ��"));

        var travelsPermission = myGroup.AddPermission(SightsServicePermissions.Travels.Default, L("�μǹ���"));
        travelsPermission.AddChild(SightsServicePermissions.Travels.Create, L("����"));
        travelsPermission.AddChild(SightsServicePermissions.Travels.Update, L("����"));
        travelsPermission.AddChild(SightsServicePermissions.Travels.Delete, L("ɾ��"));

        var activityPermission = myGroup.AddPermission(SightsServicePermissions.Activity.Default, L("�����"));
        activityPermission.AddChild(SightsServicePermissions.Activity.Create, L("����"));
        activityPermission.AddChild(SightsServicePermissions.Activity.Update, L("����"));
        activityPermission.AddChild(SightsServicePermissions.Activity.Delete, L("ɾ��"));

        var commentPermission = myGroup.AddPermission(SightsServicePermissions.Comment.Default, L("���۹���"));
        commentPermission.AddChild(SightsServicePermissions.Comment.Create, L("����"));
        commentPermission.AddChild(SightsServicePermissions.Comment.Update, L("����"));
        commentPermission.AddChild(SightsServicePermissions.Comment.Delete, L("ɾ��"));

        var sightsTravelsPermission = myGroup.AddPermission(SightsServicePermissions.SightsTravels.Default, L("�����μǹ���"));
        sightsTravelsPermission.AddChild(SightsServicePermissions.SightsTravels.Create, L("����"));
        sightsTravelsPermission.AddChild(SightsServicePermissions.SightsTravels.Update, L("����"));
        sightsTravelsPermission.AddChild(SightsServicePermissions.SightsTravels.Delete, L("ɾ��"));

        var sightsActivityPermission = myGroup.AddPermission(SightsServicePermissions.SightsActivity.Default, L("��������"));
        sightsActivityPermission.AddChild(SightsServicePermissions.SightsActivity.Create, L("����"));
        sightsActivityPermission.AddChild(SightsServicePermissions.SightsActivity.Update, L("����"));
        sightsActivityPermission.AddChild(SightsServicePermissions.SightsActivity.Delete, L("ɾ��"));

        var travelsExtentionPermission = myGroup.AddPermission(SightsServicePermissions.TravelsExtention.Default, L("�μ���չ����"));
        travelsExtentionPermission.AddChild(SightsServicePermissions.TravelsExtention.Create, L("����"));
        travelsExtentionPermission.AddChild(SightsServicePermissions.TravelsExtention.Update, L("����"));
        travelsExtentionPermission.AddChild(SightsServicePermissions.TravelsExtention.Delete, L("ɾ��"));

        var sightsTagPermission = myGroup.AddPermission(SightsServicePermissions.SightsTag.Default, L("Permission:SightsTag"));
        sightsTagPermission.AddChild(SightsServicePermissions.SightsTag.Create, L("Permission:Create"));
        sightsTagPermission.AddChild(SightsServicePermissions.SightsTag.Update, L("Permission:Update"));
        sightsTagPermission.AddChild(SightsServicePermissions.SightsTag.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SightsServiceResource>(name);
    }
}
