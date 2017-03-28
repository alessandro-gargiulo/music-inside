using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicInside.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace MusicUpdateBatch
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));
        private static IConfigurationRoot Configuration;
        private static readonly string APPFOLDERNAME = "MusicUpdateBatch";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("--------Welcome to the Music Update Batch--------");
            Console.WriteLine("--------This batch was written only for  --------");
            Console.WriteLine("--------MusicInside WebApp database      --------");
            Console.WriteLine("--------Author: Alessandro Gargiulo      --------");
            Console.WriteLine("-------------------------------------------------");

            var services = new ServiceCollection();

            string BasePath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            // Set up fon configuration sources
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(BasePath, APPFOLDERNAME))
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            // Log4net Configuration
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(System.IO.File.OpenRead("log4net.xml"));

            var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            // Dependency injection for Configuration File
            services.AddSingleton<IConfiguration>(Configuration);
            // Dependency injection for Log
            services.AddSingleton<log4net.ILog>(log);
            // Dependency injection for DBContext
            services.AddDbContext<SongDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MusicInsideDatabase")));

            services.AddTransient<Prova>();

            var provider = services.BuildServiceProvider();

            using(var prova = provider.GetService<Prova>())
            {

            }
        }
    }

    public class Prova : IDisposable
    {
        public Prova(log4net.ILog log, SongDBContext context)
        {
            log.Info("Ci sono riuscito");
            log.Info(context.Songs.Where(x => x.Title == "Albachiara").Select(y => y.Year));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}