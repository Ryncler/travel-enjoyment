using Volo.Abp.Reflection;

namespace StorageService.Permissions;

public class StorageServicePermissions
{
    public const string GroupName = "StorageService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(StorageServicePermissions));
    }
}
