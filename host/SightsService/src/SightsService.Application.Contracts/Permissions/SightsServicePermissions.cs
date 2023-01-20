using Volo.Abp.Reflection;

namespace SightsService.Permissions;

public class SightsServicePermissions
{
    public const string GroupName = "SightsService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(SightsServicePermissions));
    }

    public class Sights
    {
        public const string Default = GroupName + ".Sights";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class Travels
    {
        public const string Default = GroupName + ".Travels";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class Activity
    {
        public const string Default = GroupName + ".Activity";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class Comment
    {
        public const string Default = GroupName + ".Comment";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class SightsTravels
    {
        public const string Default = GroupName + ".SightsTravels";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class SightsActivity
    {
        public const string Default = GroupName + ".SightsActivity";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class TravelsExtention
    {
        public const string Default = GroupName + ".TravelsExtention";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}
