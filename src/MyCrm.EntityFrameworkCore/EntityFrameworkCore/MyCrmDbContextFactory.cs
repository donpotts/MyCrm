using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MyCrm.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class MyCrmDbContextFactory : IDesignTimeDbContextFactory<MyCrmDbContext>
{
    public MyCrmDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        MyCrmEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<MyCrmDbContext>()
            .UseSqlite(configuration.GetConnectionString("Default"));
        
        return new MyCrmDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MyCrm.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
