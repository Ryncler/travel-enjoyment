namespace BaseService;

public static class BaseServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "BaseService";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "BaseService";
}
