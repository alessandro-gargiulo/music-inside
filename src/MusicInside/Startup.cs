using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MusicInside.Models;
using Microsoft.EntityFrameworkCore;
using MusicInside.Data;
using MusicInside.ManagerInterfaces;
using MusicInside.DataAccessInterfaces;
using MusicInside.DataAccess;
using MusicInside.Business;

namespace MusicInside
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Dependency injection for DBContext
            services.AddDbContext<SongDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MusicInsideDatabase")));

            // Dependency injection for Configuration File
            services.AddSingleton<IConfiguration>(Configuration);

            // Dependency injection for Application Classes
            // Data Access Layer
            services.AddScoped(typeof(ISongDataAccess), typeof(SongDataAccess));
            services.AddScoped(typeof(IArtistDataAccess), typeof(ArtistDataAccess));
            services.AddScoped(typeof(IAlbumDataAccess), typeof(AlbumDataAccess));
            // Business Layer
            services.AddScoped(typeof(ISongManager), typeof(SongManager));
            services.AddScoped(typeof(IArtistManager), typeof(ArtistManager));
            services.AddScoped(typeof(IAlbumManager), typeof(AlbumManager));
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, SongDBContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Initialize(context);

            //log4net.Config.XmlConfigurator.Configure();
        }
    }
}
