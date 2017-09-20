using AutoMapper;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
	[EnableCors("SiteCorsPolicy")]
	[Authorize]
	[Route("project")]
    public class ProjectController : AuthorizedController<IProjectsService>
	{
		public ProjectController(
			IProjectsService projectService,
			IOptions<AppSettings> appSettings)
			: base(projectService, appSettings.Value)
		{
		}

		[HttpPost("")]
		public IActionResult CreateProject([FromBody]Project project)
		{
			return SecuredCall(accountId => Service.CreateProject(accountId, project));
		}

		[HttpPut("")]
		public IActionResult UpdateProject([FromBody]Project project)
		{
			return SecuredCall(accountId => Service.UpdateProject(accountId, project));
		}
		
		[HttpDelete]
		public IActionResult DeleteProject(Guid projectId)
		{
			return SecuredCall(accountId => Service.DeleteProject(accountId, projectId));
		}

		[HttpGet("{projectId}")]
		public IActionResult Get(Guid projectId)
		{
			return Ok(Service.GetProject(projectId));
		}
	}
}
