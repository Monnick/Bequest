using AutoMapper;
using BackEnd.Configuration;
using BackEnd.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
	public abstract class BasicController<T> : Controller where T : IBasicService
	{
		protected readonly T Service;
		protected readonly AppSettings AppSettings;

		protected BasicController(T service, AppSettings settings) : base()
		{
			Service = service;
			AppSettings = settings;
		}

		[AllowAnonymous]
		[HttpGet("countries")]
		public IActionResult Countries()
		{
			try
			{
				return Ok(Service.GetCountries());
			}
			catch (Exception ex)
			{
				// return error message if there was an exception
				return BadRequest(ex.Message);
			}
		}

		[AllowAnonymous]
		[HttpGet("categories")]
		public IActionResult Categories()
		{
			try
			{
				return Ok(Service.GetCountries());
			}
			catch (Exception ex)
			{
				// return error message if there was an exception
				return BadRequest(ex.Message);
			}
		}
	}
}
