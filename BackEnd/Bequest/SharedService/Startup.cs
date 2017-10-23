using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Nancy.Owin;

namespace SharedService
{
	public class Startup
	{
		public void Configure(IApplicationBuilder app)
		{
			app.UseOwin(pipeline => pipeline.UseNancy(options =>
			{
				options.Bootstrapper = new Bootstrapper(app.ApplicationServices);
			}));
		}
	}
}
