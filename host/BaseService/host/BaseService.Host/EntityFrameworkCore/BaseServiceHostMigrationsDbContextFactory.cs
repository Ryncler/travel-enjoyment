using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BaseService.EntityFrameworkCore;

public class BaseServiceHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<BaseServiceHostMigrationsDbContext>
{
    public BaseServiceHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<BaseServiceHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Default"));

        return new BaseServiceHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
