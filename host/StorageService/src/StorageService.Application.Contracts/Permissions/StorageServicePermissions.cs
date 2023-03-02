using Volo.Abp.Reflection;

namespace StorageService.Permissions;

public class StorageServicePermissions
{
    public const string GroupName = "StorageService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(StorageServicePermissions));
    }

    public class Image
    {
        public const string Default = GroupName + ".Image";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}
