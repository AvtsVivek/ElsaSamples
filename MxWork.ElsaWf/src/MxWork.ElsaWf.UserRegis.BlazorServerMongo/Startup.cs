using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
using Elsa.Extensions;
using Elsa.Persistence.MongoDb.Extensions;
using MxWork.ElsaWf.UserRegis.BlazorServerMongo.Extensions;
using MxWork.ElsaWf.UserRegis.BlazorServerMongo.Handlers;
using MxWork.ElsaWf.UserRegis.BlazorServerMongo.Models;
using MxWork.ElsaWf.UserRegis.BlazorServerMongo.Services;
using Fluid;
using Elsa.Models;
using Elsa.Persistence.MongoDb.Serialization;
using Elsa.Persistence.MongoDb.Services;
using MongoDB.Bson;
using MongoDb.Bson.NodaTime;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace MxWork.ElsaWf.UserRegis.BlazorServerMongo
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

            services
                // Add Elsa services. 
                .AddElsa(
                    elsa =>
                    {
                        // Configure Elsa to use the MongoDB provider.
                        elsa.AddMongoDbStores(Configuration, databaseName: "UserRegistration", connectionStringName: "MongoDb1");
                    })

                // Add Elsa Dashboard services.
                .AddElsaDashboard()

                // Add the activities we want to use.
                .AddEmailActivities(options => options.Bind(Configuration.GetSection("Elsa:Smtp22")))
                .AddHttpActivities(options => options.Bind(Configuration.GetSection("Elsa:Http")))
                .AddTimerActivities(options => options.Bind(Configuration.GetSection("Elsa:Timers")))
                .AddUserActivities()

                // Add our PasswordHasher service.
                .AddSingleton<IPasswordHasher, PasswordHasher>()

                // Add a MongoDB collection for our User model.
                .AddMongoDbCollection<User>("Users")
                //.AddSingleton<IMongoCollection<User>>(sp => CreateCollection<User>(sp, "Users"))

                // Add our liquid handler.
                .AddNotificationHandlers(typeof(LiquidConfigurationHandler));
        }
        // The following is not currently used.
        private static IMongoCollection<T> CreateCollection<T>(IServiceProvider serviceProvider, string collectionName)
        {
            var database = serviceProvider.GetRequiredService<IMongoDatabase>();
            return database.GetCollection<T>(collectionName);
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
                app.UseHsts();
            }

            app.UseStaticFiles();

            // Add Elsa's middleware to handle HTTP requests to workflows.  
            app.UseHttpActivities();

            app.UseRouting();

            app.UseEndpoints(
                endpoints =>
                {
                    // Blazor stuff.
                    endpoints.MapBlazorHub();
                    endpoints.MapFallbackToPage("/_Host");

                    // Attribute-based routing stuff.
                    endpoints.MapControllers();
                });
        }
    }
}
