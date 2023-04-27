using StorageService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace StorageService.Permissions;

public class StorageServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(StorageServicePermissions.GroupName, L("存储服务管理"));

        var imagePermission = myGroup.AddPermission(StorageServicePermissions.Image.Default, L("图片管理"));
        imagePermission.AddChild(StorageServicePermissions.Image.Create, L("创建"));
        imagePermission.AddChild(StorageServicePermissions.Image.Update, L("更新"));
        imagePermission.AddChild(StorageServicePermissions.Image.Delete, L("删除"));

        var minioPermission = myGroup.AddPermission(StorageServicePermissions.Minio.Default, L("Minio管理"));
        minioPermission.AddChild(StorageServicePermissions.Minio.Create, L("创建"));
        minioPermission.AddChild(StorageServicePermissions.Minio.Update, L("更新"));
        minioPermission.AddChild(StorageServicePermissions.Minio.Delete, L("删除"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<StorageServiceResource>(name);
    }
}
