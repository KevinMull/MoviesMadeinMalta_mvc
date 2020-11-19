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
            //services.AddMvc();
            services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<MaltaMoviesContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MaltaMoviesContext")));

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

        }

       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
//#pragma warning disable CS0618 // Type or member is obsolete
//            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
//#pragma warning restore CS0618 // Type or member is obsolete
//             loggerFactory.AddDebug();

            if (env.IsDevelopment())

            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {

                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}"
                   );
                 
                //Custom route for LocationSites Index ('Locations')
                routes.MapRoute(
                   name: "Locations",
                   template: "Locations",
                   defaults: new { controller = "LocationSites", action = "Index" }
               );

                //Custom route for Location Details 
                routes.MapRoute(
                   name: "LocationSite",
                   template: "{controller}/{action=Details}/{id?}/{title}",
                   defaults: new { controller = "LocationSites" }
               );



               // Standard
               // routes.MapRoute(
               //    name: "MovieId",
               //    template: "Movies/{id}",
               //    defaults: new { controller = "Movies", action = "Details" }
               //);

                //Custom route for Movie title Index ('Title')
                routes.MapRoute(
                   name: "MovieTitle",
                   template: "{controller}/{action=Details}/{id?}/{title}",
                   defaults: new { controller = "Movies" }
               );

                //Custom route for Scene Details ('Scene')

                routes.MapRoute(
                   name: "Scene",
                   template: "Scenes/Scene/{id?}",
                   defaults: new { controller = "Scenes", action = "Details" }
               );

                //Custom route for Movies api
               // routes.MapRoute(
               //    name: "MovieApi",
               //    template: "api/movies",
               //    defaults: new { controller = "Moviesapi", action = "Index" }
               //);
                //Custom route for Movie by id api
               // routes.MapRoute(
               //    name: "MoviesApiId",
               //    template: "api/movies/{id}",
               //    defaults: new { controller = "Moviesapi", action = "Details" }
               //);
                       
            }

           );
           
        }
        
    }
}
