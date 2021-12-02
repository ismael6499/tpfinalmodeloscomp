using System;
using BrazoAPI.Data;
using Microsoft.Extensions.Configuration;

namespace BrazoAPI
{
    class Program
    {
        public static IConfiguration configuration;

        static void Main(string[] args)
        {
            configuration = CreateConfiguration(args);

            BrazoService brazoService = new BrazoService();
            brazoService.init(configuration);
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