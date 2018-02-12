using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace Pharmacy.IntegrationTests
{
    public class Program
    {
        public static IWebHost BuildWebHost(string appRootPath, string[] args)
        {
            var webHostBuilder = GetWebHostBuilder(appRootPath, args);
            return webHostBuilder.Build();
        }


        public static IWebHostBuilder GetWebHostBuilder(string appRootPath, string[] args)
        {
            var webHostBuilder = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(appRootPath)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var secretsMode = GetSecretsMode(hostingContext.HostingEnvironment);
                    config.AddGovrnanzaConfig(secretsMode, "REGISTRY_CONFIG_FILE");
                })
                .UseStartup<Startup>();

            return webHostBuilder;
        }
    }
}
