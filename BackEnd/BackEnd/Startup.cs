using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BackEnd.Contexts;
using BackEnd.Services.Contracts;
using BackEnd.Services;
using BackEnd.Configuration;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace BackEnd
{
    public class Startup
	{
		public IConfigurationRoot Configuration { get; }

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
        {
			// cors
			var corsBuilder = new CorsPolicyBuilder();
			corsBuilder.AllowAnyHeader();
			corsBuilder.AllowAnyMethod();
			corsBuilder.AllowAnyOrigin(); // For anyone access.
										  //corsBuilder.WithOrigins("http://localhost:56573"); // for a specific url. Don't add a forward slash on the end!
			corsBuilder.AllowCredentials();

			services.AddCors(options =>
			{
				options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
			});

			services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddMvc();
			services.AddAutoMapper();

			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

			// configure DI for application services
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<IProjectInfoService, ProjectInfoService>();
			services.AddScoped<IProjectOverviewService, ProjectOverviewService>();
			services.AddScoped<IProjectsService, ProjectsService>();
			services.AddScoped<IContentService, ContentService>();
			services.AddScoped<IPictureService, PictureService>();
			services.AddScoped<INeededItemsService, NeededItemsService>();

			var appSettings = services.AddSingleton(Configuration.GetSection("AppSettings").Get<AppSettings>()).Single(s => s.ServiceType == typeof(AppSettings)).ImplementationInstance as AppSettings;

			// configure jwt authentication
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(o =>
				{
					o.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			app.SeedCountries();

			app.UseDeveloperExceptionPage();
			
			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=overview}/{action=Latest}");
			});

			app.UseCors("SiteCorsPolicy");
		}
    }
}
