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

    public class OpenIddict
    {
        public const string Default = GroupName + ".OpenIddict";
        public const string UpdateScope = Default + ".UpdateScope";
        public const string CreateScope = Default + ".CreateScope";
        public const string DeleteScope = Default + ".DeleteScope";

        public const string UpdateApplication = Default + ".UpdateApplication";
        public const string CreateApplication = Default + ".CreateApplication";
        public const string DeleteApplication = Default + ".DeleteApplication";
    }

    public class UserManage
    {
        public const string Default = GroupName + ".UserManage";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}
