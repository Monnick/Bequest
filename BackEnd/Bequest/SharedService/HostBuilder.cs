using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SharedService
{
    public class HostBuilder<TStartup> where TStartup : class
    {
		private IWebHost _host;

		public HostBuilder(params string[] urls)
		{
			_host = new WebHostBuilder()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseKestrel()
				.UseUrls(urls)
				.UseStartup<TStartup>()
				.Build();
		}

		public void Run()
		{
			_host.Run();
		}
    }
}
