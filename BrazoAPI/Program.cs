using System;
using BrazoAPI.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Owin.Hosting;

namespace BrazoAPI
{
    class Program
    {
        public static IConfiguration configuration;
        public static BrazoService brazoService;

        static void Main(string[] args)
        {
            configuration = CreateConfiguration(args);
            brazoService = new BrazoService();
            brazoService.init(configuration);
            
            string url = configuration["urls:api"];
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Se está ejecutando nancy en {url}");
                Console.ReadKey();
            }
        }

       
        private static IConfiguration CreateConfiguration(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
            return configuration;
        }

        public static IConfiguration Configuration
        {
            get => configuration;
            set => configuration = value;
        }
    }
}