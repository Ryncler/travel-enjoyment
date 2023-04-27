using Volo.Abp.Reflection;

namespace BaseService.Permissions;

public class BaseServicePermissions
{
    public const string GroupName = "BaseService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(BaseServicePermissions));
    }

    /// <summary>
    /// OpenIddict
    /// </summary>
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

    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserManage
    {
        public const string Default = GroupName + ".UserManage";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    /// <summary>
    /// 入驻管理
    /// </summary>
    public class EntryInfo
    {
        public const string Default = GroupName + ".EntryInfo";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class AuditLog
    {
        public const string Default = GroupName + ".AuditLog";
    }
}
