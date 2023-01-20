using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SightsService.EntityFrameworkCore;

public class SightsServiceHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<SightsServiceHttpApiHostMigrationsDbContext>
{
    public SightsServiceHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<SightsServiceHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("SightsService"));

        return new SightsServiceHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
