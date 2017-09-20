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
	[Route("pictures")]
	public class PicturesController : AuthorizedController<IPictureService>
	{
		public PicturesController(
			IPictureService pictureService,
			IOptions<AppSettings> appSettings)
			: base(pictureService, appSettings.Value)
		{
		}

		[HttpPost("")]
		public IActionResult UpdatePicture()
		{
			try
			{
				Guid projectId;

				var projectkeyValue = Request.Form.FirstOrDefault(f => f.Key.Equals("projectId", StringComparison.InvariantCultureIgnoreCase));

				if (string.IsNullOrEmpty(projectkeyValue.Value))
					return NotFound("Project not found");
				projectId = Guid.Parse(projectkeyValue.Value);

				var files = Request.Form.Files;
				foreach (var file in files)
				{
					//TODO: do security checks ...!

					if (file == null || file.Length == 0)
					{
						continue;
					}

					var fileStream = file.OpenReadStream();

					return Ok(Service.UpdatePicture(projectId, fileStream));
				}

				return this.BadRequest("No file transmitted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[AllowAnonymous]
		[HttpGet("{projectId}")]
		public IActionResult Get(Guid projectId)
		{
			try
			{
				return File(Service.GetPicture(projectId), "image/jpeg");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
