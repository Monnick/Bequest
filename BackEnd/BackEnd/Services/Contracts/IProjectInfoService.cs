using BackEnd.Entities;
using BackEnd.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Contracts
{
    public interface IProjectInfoService : IBasicService
	{
		IEnumerable<Guid> GetProjectIds(Guid accountId);

		IEnumerable<ProjectInfo> GetProjects(Guid accountId);

		ProjectInfo GetProject(Guid projectId);

		void SetProjectState(Guid accountId, Guid projectId, State state);
    }
}
