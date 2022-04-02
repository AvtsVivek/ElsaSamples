using Elsa.Persistence.EntityFrameworkCore.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MxWork.ElsaWf.UserRegis.BlazorServerMsSql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elsa.Persistence.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Elsa.Activities.Email.Extensions;
using Elsa.Activities.Http.Extensions;
using Elsa.Activities.Timers.Extensions;
using Elsa.Dashboard.Extensions;
using MxWork.ElsaWf.UserRegis.BlazorServerMsSql.Extensions;
using MxWork.ElsaWf.UserRegis.BlazorServerMsSql.Services;
using Elsa.Extensions;
using MxWork.ElsaWf.UserRegis.BlazorServerMsSql.Handlers;

namespace MxWork.ElsaWf.UserRegis.BlazorServerMsSql
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
            services.AddRazorPages();
            services.AddServerSideBlazor();

            //services.AddSingleton<WeatherForecastService>();



            services
                .AddElsa(
                    elsa => {
                        elsa.AddEntityFrameworkStores<SqlServerContext>(ef => ef.UseSqlServer(Configuration.GetConnectionString("SqlServerExtended")));
                       })
                //.AddElsa().AddElsaDashboard()
                .AddHttpActivities(options => options.Bind(Configuration.GetSection("Elsa:Http")))
                .AddEmailActivities(options => options.Bind(Configuration.GetSection("Elsa:Smtp22")))
                .AddTimerActivities(options => options.Bind(Configuration.GetSection("Elsa:Timers")))
                

            //services
            //// Add Elsa services. 
            //.AddElsa(
            //    elsa =>
            //    {
            //                    // Configure Elsa to use the MongoDB provider.
            //                    elsa.AddMongoDbStores(Configuration, databaseName: "UserRegistration", connectionStringName: "MongoDb1");
            //    })

            // Add Elsa Dashboard services.
            .AddElsaDashboard()

            // Add the activities we want to use.
            //.AddEmailActivities(options => options.Bind(Configuration.GetSection("Elsa:Smtp22")))
            //.AddHttpActivities(options => options.Bind(Configuration.GetSection("Elsa:Http")))
            //.AddTimerActivities(options => options.Bind(Configuration.GetSection("Elsa:Timers")))
            .AddUserActivities()

            // Add our PasswordHasher service.
            .AddSingleton<IPasswordHasher, PasswordHasher>()

            // The followng is the question mark
            // Add a MongoDB collection for our User model.
            //.AddMongoDbCollection<User>("Users")
            //.AddSingleton<IMongoCollection<User>>(sp => CreateCollection<User>(sp, "Users"))

            // Add our liquid handler.
            .AddNotificationHandlers(typeof(LiquidConfigurationHandler));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");

                // Attribute-based routing stuff.
                endpoints.MapControllers();
            });
        }
    }
}
