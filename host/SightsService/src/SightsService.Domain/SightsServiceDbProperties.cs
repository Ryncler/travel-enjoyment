namespace SightsService;

public static class SightsServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "SightsService";
}
