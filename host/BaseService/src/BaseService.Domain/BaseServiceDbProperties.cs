namespace BaseService;

public static class BaseServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "Ryncler";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "BaseService";
}
