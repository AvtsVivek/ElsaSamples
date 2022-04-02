using System;
using System.IO;
using Elsa.Persistence.EntityFrameworkCore.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace MxWork.ElsaWf.WebMsSql
{
    public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerContext>
    {
        public SqlServerContext CreateDbContext(string[] args)
        {

            var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{ currentEnv ?? "Production"}.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("MsSqlServerDb");

            var optionsBuilder = new DbContextOptionsBuilder<SqlServerContext>();
            //var migrationAssembly = typeof(SqlServerContext).Assembly.FullName;
            var migrationAssembly = this.GetType().Assembly.FullName;

            if (connectionString == null)
                throw new InvalidOperationException("Set the EF_CONNECTIONSTRING environment variable to a valid SQL Server connection string. E.g. SET EF_CONNECTIONSTRING=Server=localhost;Database=Elsa;User=sa;Password=Secret_password123!;");

            optionsBuilder.UseSqlServer(
                connectionString,
                x => x.MigrationsAssembly(migrationAssembly)
            );

            return new SqlServerContext(optionsBuilder.Options);
        }
    }
}

// This class is needed for database table creation, required for ms sql.
// Add-Migration - StartupProject MxWork.Elsa.WebSqLite - Context "SqlServerContext" InitialMigration
// Or else this should also work. 
// Add-Migration InitialMigration -o SqlServerMigrations -Context SqlServerContext
// Then we can update the database.
// Update-Database
   