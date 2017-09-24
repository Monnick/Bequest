using BackEnd.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Contracts
{
    public interface IProjectOverviewService : IBasicService
	{
		ProjectView Get(Guid projectId);

		IEnumerable<ProjectThumbnail> GetProjects(int pageNumber, int pageSize, OrderBy order, string category, string country);
	}
}
