using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SharedService;
using SharedService.Configuration;
using System;
using System.IO;
using UserStorage.SQL.Contexts;

namespace UserService
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
			Bootstrapper.RegisterSingleton(new UserContext(defaultConnection));

			new HostBuilder<Startup>("http://*:8888").Run();
		}
	}
}
