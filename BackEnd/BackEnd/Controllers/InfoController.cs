﻿using AutoMapper;
using BackEnd.Configuration;
using BackEnd.Entities;
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
	[Route("info")]
    public class InfoController : AuthorizedController<IProjectInfoService>
	{
		public InfoController(
			IProjectInfoService projectService,
			IOptions<AppSettings> appSettings)
			: base(projectService, appSettings.Value)
		{
		}
		
		[HttpGet("")]
		public IActionResult GetAll()
		{
			return SecuredCall(accountId => Service.GetProjects(accountId));
		}
		
		//[HttpPut("{projectId}/{state}")]
		//public IActionResult SetState(Guid projectId, State state)
		//{
		//	return SecuredCall(accountId => Service.SetProjectState(accountId, projectId, state));
		//}

		[HttpPut("")]
		public IActionResult UpdateItems([FromBody] NeededItems items)
		{
			return SecuredCall(accountId => Service.UpdateNeededItems(items));
		}
	}
}
