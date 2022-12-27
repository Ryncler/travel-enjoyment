using Volo.Abp.Reflection;

namespace BaseService.Permissions;

public class BaseServicePermissions
{
    public const string GroupName = "BaseService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(BaseServicePermissions));
    }

    public class UserExtension
    {
        public const string Default = GroupName + ".UserExtension";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}
