using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace MusicUpdateBatch
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));
        private static IConfigurationRoot Configuration;
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("--------Welcome to the Music Update Batch--------");
            Console.WriteLine("--------This batch was written only for  --------");
            Console.WriteLine("--------MusicInside WebApp database      --------");
            Console.WriteLine("--------Author: Alessandro Gargiulo      --------");
            Console.WriteLine("-------------------------------------------------");

            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (String.IsNullOrWhiteSpace(environment))
                throw new ArgumentNullException("Environment not found in ASPNETCORE_ENVIRONMENT");

            Console.WriteLine("Environment: {0}", environment);

            var services = new ServiceCollection();

            // Set up fon configuration sources
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true);

            Configuration = builder.Build();

            // Log4net Configuration
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(System.IO.File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            // Dependency injection for Configuration File
            services.AddSingleton<IConfiguration>(Configuration);
            // Dependency injection for Log
            services.AddSingleton<log4net.ILog>(log);
        }
    }

    public static class ServicesExtension
    {
        public static IServiceCollection AddMusicInside(this IServiceCollection services)
        {
            return services;
        }
    }
}