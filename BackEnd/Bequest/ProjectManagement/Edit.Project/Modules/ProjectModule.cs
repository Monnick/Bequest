using Edit.Project.Services;
using FakeBus.Contracts;
using Nancy.ModelBinding;
using Project.Commands.Project;
using Project.Commands.Project.Validation;
using SharedService.Configuration;
using SharedService.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edit.Project.Modules
{
    public class ProjectModule : AuthorizedModule
	{
		private ProjectService _service;

		public ProjectModule(AppSettings settings, ProjectService service)
			: base("/projectedit", settings)
		{
			_service = service;

			Post("/", _ => CreateProject());
			Put("/", _ => UpdateProject());
		}
		
		private dynamic CreateProject()
		{
			try
			{
				if (!isAuthenticated())
					return Unauthorized();
				
				var data = this.Bind<Dictionary<string, string>>();

				var cmd = new CreateProject(Guid.Parse(data["id"]), data["title"], data["category"], data["creator"]);

				var validator = new CreateProjectValidator();
				validator.Validate(cmd);

				_service.CreateProject(cmd);

				return NotImplemented();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		private dynamic UpdateProject()
		{
			try
			{
				if (!isAuthenticated())
					return Unauthorized();

				return NotImplemented();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
