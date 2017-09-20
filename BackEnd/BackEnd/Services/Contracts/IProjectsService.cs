using BackEnd.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Contracts
{
	public interface IProjectsService : IBasicService
	{
		Guid CreateProject(Guid accountId, Project project);

		Guid UpdateProject(Guid accoutId, Project project);

		void DeleteProject(Guid accountId, Guid projectId);

		Project GetProject(Guid projectId);
	}
}
