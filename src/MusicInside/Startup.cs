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
using System.Xml;
using System.Reflection;

namespace MusicInside
{
    public class Startup
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            // Log4net Configuration
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(System.IO.File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
            log.Info("Music Inside Web App | Application Started.");
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
            // Dependency injection for Log
            services.AddSingleton<log4net.ILog>(log);

            // Dependency injection for Application Classes
            // Data Access Layer
            services.AddScoped(typeof(ISongDataAccess), typeof(SongDataAccess));
            services.AddScoped(typeof(IArtistDataAccess), typeof(ArtistDataAccess));
            services.AddScoped(typeof(IAlbumDataAccess), typeof(AlbumDataAccess));
            services.AddScoped(typeof(IGenreDataAccess), typeof(GenreDataAccess));
            services.AddScoped(typeof(IStatisticDataAccess), typeof(StatisticDataAccess));
            services.AddScoped(typeof(IFileDataAccess), typeof(FileDataAccess));
            // Business Layer
            services.AddScoped(typeof(ISongManager), typeof(SongManager));
            services.AddScoped(typeof(IArtistManager), typeof(ArtistManager));
            services.AddScoped(typeof(IAlbumManager), typeof(AlbumManager));
        }

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
        }
    }
}
