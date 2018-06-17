using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicInside.Managers.Context;
using MusicInside.Managers.Implementations;
using MusicInside.Managers.Interfaces;
using System;
using System.Reflection;
using System.Xml;

namespace MusicInside.WebApi
{
    public class Startup
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Log4net Configuration
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(System.IO.File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
            log.InfoFormat("Music Inside Web App | Application Started | Date = {0}", DateTime.Now);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<MusicInsideDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MusicInsideDatabase")));

            // Dependency injection for Manager Classes
            services.AddScoped(typeof(ISongManager), typeof(SongManager));
            services.AddScoped(typeof(IAlbumManager), typeof(AlbumManager));
            services.AddScoped(typeof(IArtistManager), typeof(ArtistManager));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
