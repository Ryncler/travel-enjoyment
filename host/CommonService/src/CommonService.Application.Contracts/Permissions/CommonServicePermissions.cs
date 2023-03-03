using Volo.Abp.Reflection;

namespace CommonService.Permissions;

public class CommonServicePermissions
{
    public const string GroupName = "CommonService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CommonServicePermissions));
    }

    public class GeoInfo
    {
        public const string Default = GroupName + ".GeoInfo";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class Tag
    {
        public const string Default = GroupName + ".Tag";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class Category
    {
        public const string Default = GroupName + ".Category";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class UserTravel
    {
        public const string Default = GroupName + ".UserTravel";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}
