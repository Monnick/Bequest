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
	[Route("content")]
	public class ContentController : AuthorizedController<IContentService>
	{
		public ContentController(
			IContentService contentService,
			IOptions<AppSettings> appSettings)
			: base(contentService, appSettings.Value)
		{
		}

		[HttpPut("")]
		public IActionResult UpdateContent([FromBody]Content content)
		{
			try
			{
				return Ok(Service.UpdateContent(content.ProjectId, content.Data));
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		
		[HttpGet("{projectId}")]
		public IActionResult Get(Guid projectId)
		{
			try
			{
				return Ok(Service.GetContent(projectId));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
