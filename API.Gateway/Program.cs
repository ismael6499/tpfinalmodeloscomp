using System;
using System.IO;
using ApiGateway;
using Microsoft.Extensions.Configuration;
using Microsoft.Owin.Hosting;

namespace API.Gateway
{
    class Program
    {
        public static IConfiguration configuration;

        static void Main(string[] args)
        {
             configuration = CreateConfiguration(args);
             string url = configuration["url"];
             using (WebApp.Start<Startup>(url))
             {
                 Console.WriteLine($"Se está ejecutando nancy en {url}");
                 Console.ReadKey();
             }
            
        }

        private static IConfiguration CreateConfiguration(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, reloadOnChange: true)
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