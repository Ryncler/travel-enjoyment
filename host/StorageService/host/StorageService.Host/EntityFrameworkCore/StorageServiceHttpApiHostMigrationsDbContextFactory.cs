using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace StorageService.EntityFrameworkCore;

public class StorageServiceHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<StorageServiceHttpApiHostMigrationsDbContext>
{
    public StorageServiceHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<StorageServiceHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("StorageService"));

        return new StorageServiceHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
