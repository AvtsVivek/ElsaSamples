using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Elsa.Activities.Email.Extensions;
using Elsa.Activities.Http.Extensions;
using Elsa.Activities.Timers.Extensions;
using Elsa.Dashboard.Extensions;
using Elsa.Persistence.EntityFrameworkCore.DbContexts;
using Elsa.Persistence.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MxWork.ElsaWf.WebMsSql
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services
            //    // Add services used for the workflows runtime.
            //    .AddElsa(elsa => elsa.AddEntityFrameworkStores<SqliteContext>(options => options.UseSqlite(Configuration.GetConnectionString("Sqlite"))))
            //    .AddHttpActivities(options => options.Bind(Configuration.GetSection("Elsa:Http")))
            //    .AddEmailActivities(options => options.Bind(Configuration.GetSection("Elsa:Smtp")))
            //    .AddTimerActivities(options => options.Bind(Configuration.GetSection("Elsa:Timers")))

            //    // Add services used for the workflows dashboard.
            //    .AddElsaDashboard();

            var sqliteConnectionString = Configuration.GetConnectionString("Sqlite");
            var msSqlServerConnectionString = Configuration.GetConnectionString("MsSqlServerDb");

            //services.AddElsa(
            //    elsa => elsa.AddEntityFrameworkStores<SqliteContext>(
            //        options => options.UseSqlite(sqliteConnectionString)));


            services.AddElsa(
                elsa => elsa.AddEntityFrameworkStores<SqlServerContext>(
                    options => {
                        var migrationAssembly = typeof(SqlServerContext).Assembly.FullName;
                        options.UseSqlServer(msSqlServerConnectionString
                            // The below line is optioinal.
                            , b => b.MigrationsAssembly(migrationAssembly)
                            );
                    }));

            services.AddHttpActivities(options => options.Bind(Configuration.GetSection("Elsa:Http")));
            services.AddEmailActivities(options => options.Bind(Configuration.GetSection("Elsa:Smtp")));
            services.AddTimerActivities(options => options.Bind(Configuration.GetSection("Elsa:Timers")));
            //    Add services used for the workflows dashboard.
            services.AddElsaDashboard();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});


            app
                .UseStaticFiles()
                .UseHttpActivities()
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .UseWelcomePage();

        }
    }
}
