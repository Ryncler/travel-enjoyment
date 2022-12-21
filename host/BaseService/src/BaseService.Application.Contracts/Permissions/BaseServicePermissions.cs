using Volo.Abp.Reflection;

namespace BaseService.Permissions;

public class BaseServicePermissions
{
    public const string GroupName = "BaseService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(BaseServicePermissions));
    }
}
