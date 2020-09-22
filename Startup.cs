using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

using TestAppConfig;

namespace TestAppConfig
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {  // method to bind configuration data to the settings class
             services.Configure<Settings>(Configuration.GetSection("TestApp:Settings"));
             services.AddControllersWithViews();
             services.AddAzureAppConfiguration();
             // adding feature flag support by calling the function below :)
             // we retrieve the flags from the FeatureManagement.. then we tell the  feature manager to read from a 
             // section called my feature flags
             services.AddFeatureManagement();
             
             // you can include filter to be used with the feature flag
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // adding the middleware to allow the configuration settings registered to be 
            // updated while the asp.core net web continues to receive requests.
            // this middleware uses the function specified in program.cs to trigger refresh for each 
            // request received by the app.
            //For each request a refresh operation is triggered and the client lib checks if the cached value for the 
            // registered conf setting has expired 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // Add the following line:
        app.UseAzureAppConfiguration();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
}
    }
}
