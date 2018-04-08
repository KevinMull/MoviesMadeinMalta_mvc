using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MaltaMoviesMVCcore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaltaMoviesMVCcore
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
        {
            services.AddMvc();
            services.AddDbContext<MaltaMoviesContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MaltaMoviesContext")));
        }
                   
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())

            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                //Custom route for LocationSites Index ('Locations')
                routes.MapRoute(
                   name: "Locations",
                   template: "Locations",
                   defaults: new { controller = "LocationSites", action = "Index" }
               );

                //Custom route for Location Details ('Location')
                routes.MapRoute(
                   name: "Location",
                   template: "LocationSites/Location/{id?}",
                   defaults: new { controller = "LocationSites", action = "Details" }
               );


                
                    //Custom route for Movie title Index ('Title')
                    routes.MapRoute(
                       name: "Title",
                       template: "Movies/Title/{id?}",
                       defaults: new { controller = "Movies", action = "Details" }
                   );

                //Custom route for Scene Details ('Scene')

                routes.MapRoute(
                   name: "Scene",
                   template: "Scenes/Scene/{id?}",
                   defaults: new { controller = "Scenes", action = "Details" }
               );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                    //TODO:
                    routes.MapRoute(
                    name: "MovieTitle",
                    template: "Movies/{Title}",
                    defaults: new { controller = "Movies", action = "Title" }
                        );


                
            }

           );
           
        }
    }
}
