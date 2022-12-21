using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BaseService.EntityFrameworkCore;

public class BaseServiceHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<BaseServiceHttpApiHostMigrationsDbContext>
{
    public BaseServiceHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<BaseServiceHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Default"));

        return new BaseServiceHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
