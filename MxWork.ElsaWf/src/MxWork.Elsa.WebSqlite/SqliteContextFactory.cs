using System;
using System.IO;
using Elsa.Persistence.EntityFrameworkCore.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MxWork.ElsaWf.WebSqlite
{
    public class SqliteContextFactory : IDesignTimeDbContextFactory<SqliteContext>
    {
        public SqliteContext CreateDbContext(string[] args)
        {

            var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{ currentEnv ?? "Production"}.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<SqlServerContext>();
            var connectionString = configuration.GetConnectionString("Sqlite");

            var optionsBuilder = new DbContextOptionsBuilder<SqliteContext>();
            //var migrationAssembly = typeof(SqliteContext).Assembly.FullName;
            var migrationAssembly = this.GetType().Assembly.FullName;

            optionsBuilder.UseSqlite(connectionString,
                x => x.MigrationsAssembly(migrationAssembly)
            );

            return new SqliteContext(optionsBuilder.Options);
        }
    }

}
