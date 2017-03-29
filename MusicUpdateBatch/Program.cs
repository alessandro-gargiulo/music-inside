using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicInside.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
            #region Program Info
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("--------Welcome to the Music Update Batch--------");
            Console.WriteLine("--------This batch was written only for  --------");
            Console.WriteLine("--------MusicInside WebApp database      --------");
            Console.WriteLine("--------Author: Alessandro Gargiulo      --------");
            Console.WriteLine("-------------------------------------------------");
            #endregion

            #region Environment Configuration With Dependency Injection
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
            // Add Business class to container
            services.AddTransient<Business.FlowHelper>();
            var provider = services.BuildServiceProvider();
            #endregion

            #region Update Process
            // Start process here
            using (var flow = provider.GetService<Business.FlowHelper>())
            {
                List<string> subFolders = flow.GetValidSubFolders();
                log.Info("MusicUpdateBatch | MainProgram: Attempt to loop over " + subFolders.Count + " folders");
                foreach(string folder in subFolders)
                {
                    List<string> fileNameList = flow.GetValidFileNameInFolder(folder);
                    log.Info("MusicUpdateBatch | MainProgram: In folder /" + folder + " - Found " + fileNameList.Count + " files");
                    foreach(string file in fileNameList)
                    {
                        var fileTag = flow.GetTagFromFileNameInFolder(folder, file);
                    }
                }
            }
            #endregion
        }
    }
}