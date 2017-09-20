using AutoMapper;
using BackEnd.Configuration;
using BackEnd.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
	[Route("overview")]
    public class OverviewController : BasicController<IProjectOverviewService>
	{
		const int PAGE_SIZE = 10;

		public OverviewController(
			IProjectOverviewService projectService,
			IOptions<AppSettings> appSettings)
			: base(projectService, appSettings.Value)
		{
		}
		
		[HttpGet("{projectId}")]
		public IActionResult Get(Guid projectId)
		{
			return Ok(Service.Get(projectId));
		}

		[Route("Latest")]
		[HttpGet]
		public IActionResult Latest()
		{
			return Ok(Service.GetLatestProjects(PAGE_SIZE, 0));
		}

		[Route("Top")]
		[HttpGet()]
		public IActionResult Top()
		{
			return Ok(Service.GetTopProjects(PAGE_SIZE, 0));
		}

		[HttpPost("GetByCountry/{country}")]
		public IActionResult GetByCountry(string country)
		{
			if (string.IsNullOrEmpty(country))
				BadRequest("Country is needed.");

			return Ok(Service.GetProjectsByCountry(PAGE_SIZE, 0, country));
		}
	}
}
