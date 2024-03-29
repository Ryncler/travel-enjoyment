﻿using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CommonService.EntityFrameworkCore;

public class CommonServiceHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<CommonServiceHttpApiHostMigrationsDbContext>
{
    public CommonServiceHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<CommonServiceHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("CommonService"));

        return new CommonServiceHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
