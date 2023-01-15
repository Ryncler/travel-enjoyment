namespace StorageService;

public static class StorageServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "StorageService";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "StorageService";
}
