using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Chat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls();
                    webBuilder.UseKestrel(options =>
                    {
                        var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
                        options.Listen(IPAddress.Any, config.GetValue<int>("Host:Port"));
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
