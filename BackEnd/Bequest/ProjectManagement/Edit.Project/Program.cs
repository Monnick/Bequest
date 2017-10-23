﻿using FakeBus.Contracts;
using Microsoft.Extensions.Configuration;
using SharedService;
using SharedService.Configuration;
using System;
using System.IO;

namespace Edit.Project
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

			var publisher = new FakeBus.Publish.Publisher(null);

			Bootstrapper.RegisterSingleton(new AppSettings(secret));
			Bootstrapper.RegisterSingleton<IPublisher>(publisher);

			new HostBuilder<Startup>("http://*:8888").Run();
		}
    }
}
