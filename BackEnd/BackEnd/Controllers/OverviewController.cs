using AutoMapper;
using BackEnd.Configuration;
using BackEnd.Services.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
	[EnableCors("SiteCorsPolicy")]
	[Route("overview")]
    public class OverviewController : BasicController<IProjectOverviewService>
	{
		const int DEFAULT_PAGE_SIZE = 10;

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
		
		[HttpGet]
		public IActionResult Get([FromQuery]string category, [FromQuery]string country, [FromQuery]string orderBy, [FromQuery]string page, [FromQuery]string size)
		{
			int pageNumber = 0;
			int pageSize = 0;
			OrderBy order = string.IsNullOrEmpty(orderBy) ? OrderBy.Undefined : orderBy == "mv" ? OrderBy.Views : OrderBy.Updated;

			if (!int.TryParse(page, out pageNumber))
				pageNumber = 0;

			if (!int.TryParse(size, out pageSize))
				pageSize = DEFAULT_PAGE_SIZE;

			int offset = pageNumber * pageSize;

			return Ok(Service.GetProjects(offset, pageSize, order, category, country));
		}
	}
}
