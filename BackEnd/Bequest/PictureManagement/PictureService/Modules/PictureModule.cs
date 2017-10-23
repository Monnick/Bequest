using Nancy;
using Nancy.Responses;
using PictureStorage.Services.Contracts;
using SharedService.Configuration;
using SharedService.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureService.Modules
{
    public class PictureModule : AuthorizedModule
	{
		private IPictureService _service;

		public PictureModule(IPictureService service, AppSettings settings)
			: base("/picture", settings)
		{
			_service = service;

			Get("/{id:guid}", args => { if (args == null) return (Response)HttpStatusCode.BadGateway; return GetPicture(args.id); });
			Post("/", _ => UpdatePicture());
		}

		private dynamic UpdatePicture()
		{
			try
			{
				if (!isAuthenticated())
					return Unauthorized();

				Guid projectId;
				
				var projectkeyValue = Request.Form["projectId"];

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

					return Ok(_service.UpdatePicture(projectId, fileStream));
				}

				return this.BadRequest("No file transmitted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		private dynamic GetPicture(Guid projectId)
		{
			try
			{
				var file = new MemoryStream(_service.GetPicture(projectId));

				var response = new StreamResponse(() => file, "image/jpeg");

				return response.AsAttachment(projectId.ToString());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
