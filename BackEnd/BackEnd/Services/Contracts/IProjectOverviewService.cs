using BackEnd.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Contracts
{
    public interface IProjectOverviewService : IBasicService
	{
		ProjectThumbnail Get(Guid projectId);

		IEnumerable<Guid> GetLatestProjects(int count, int offset);

		IEnumerable<Guid> GetTopProjects(int count, int offset);

		IEnumerable<Guid> GetProjectsByCountry(int count, int offset, string country);
	}
}
