using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SharedService;
using System;
using System.IO;
using PictureStorage.SQL.Contexts;
using SharedService.Configuration;

namespace PictureService
{
    class Program
    {
        static void Main(string[] args)
		{
			var builder = new ConfigurationBuilder()
				   .SetBasePath(Directory.GetCurrentDirectory())
				   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				   .AddEnvironmentVariables();

			var configuration = builder.Build();
			
			string secret = configuration.GetSection("secret").Value;
			string defaultConnection = configuration.GetConnectionString("DefaultConnection");

			Bootstrapper.RegisterSingleton(new AppSettings(secret));
			Bootstrapper.RegisterSingleton(new PictureContext(defaultConnection));

			new HostBuilder<Startup>("http://*:8888").Run();
		}
    }
}
