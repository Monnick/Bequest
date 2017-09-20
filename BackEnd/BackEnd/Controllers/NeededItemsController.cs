using BackEnd.Configuration;
using BackEnd.Models.Projects;
using BackEnd.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
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
	[Authorize]
	[Route("items")]
	public class NeededItemsController : AuthorizedController<INeededItemsService>
	{
		public NeededItemsController(
			INeededItemsService itemsService,
			IOptions<AppSettings> appSettings)
			: base(itemsService, appSettings.Value)
		{
		}

		[HttpPut("")]
		public IActionResult UpdateNeededItems([FromBody]NeededItems items)
		{
			try
			{
				return Ok(Service.UpdateItems(items));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{projectId}")]
		public IActionResult Get(Guid projectId)
		{
			try
			{
				return Ok(Service.GetItems(projectId));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
