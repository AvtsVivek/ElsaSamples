using Elsa.Activities.Email.Extensions;
using Elsa.Activities.Http.Extensions;
using Elsa.Activities.Timers.Extensions;
using Elsa.Dashboard.Extensions;
using Elsa.Persistence.EntityFrameworkCore.DbContexts;
using Elsa.Persistence.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MxWork.ElsaWf.WebSqlite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                   // Add services used for the workflows runtime.
                   .AddElsa(elsa => elsa.AddEntityFrameworkStores<SqliteContext>(options => options.UseSqlite(Configuration.GetConnectionString("Sqlite"))))
                   .AddHttpActivities(options => options.Bind(Configuration.GetSection("Elsa:Http")))
                   .AddEmailActivities(options => options.Bind(Configuration.GetSection("Elsa:Smtp")))
                   .AddTimerActivities(options => options.Bind(Configuration.GetSection("Elsa:Timers")))

                   // Add services used for the workflows dashboard.
                   .AddElsaDashboard();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app
                .UseStaticFiles()
                .UseHttpActivities()
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .UseWelcomePage();
        }
    }
}
