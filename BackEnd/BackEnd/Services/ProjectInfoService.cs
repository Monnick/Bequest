using BackEnd.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Entities;
using BackEnd.Models.Projects;
using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services
{
	public class ProjectInfoService : BasicService<DataContext>, Contracts.IProjectInfoService
	{
		public ProjectInfoService(DataContext context) : base(context)
		{
		}

		public IEnumerable<Guid> GetProjectIds(Guid accountId)
		{
			return Context.Projects.Where(p => p.InitiatorId == accountId).Select(p => p.Id);
		}

		public IEnumerable<ProjectInfo> GetProjects(Guid accountId)
		{
			return Context.Projects.Where(p => p.InitiatorId == accountId).Select(p => new ProjectInfo
			{
				Id = p.Id,
				Title = p.Title,
				Category = p.Category.Title,
				UpdatedAt = p.UpdatedAt,
				State = p.State,
				PossibleStates = p.State.GetPossibleStates(),
				Views = p.Views
			});
		}

		public ProjectInfo GetProject(Guid projectId)
		{
			var project = Context.Projects.Include(p => p.Category).Single(p => p.Id == projectId);
			
			return new ProjectInfo
			{
				Id = project.Id,
				Title = project.Title,
				Category = project.Category == null ? "" : project.Category.Title,
				UpdatedAt = project.UpdatedAt,
				State = project.State,
				PossibleStates = project.State.GetPossibleStates(),
				Views = project.Views
			};
		}

		public void SetProjectState(Guid accountId, Guid projectId, State state)
		{
			if (!Context.Projects.Any(p => p.Id == projectId && p.InitiatorId == accountId))
				throw new AuthenticationException();

			Entities.Projects.Project temp = new Entities.Projects.Project() { Id = projectId, State = state };

			Context.Projects.Attach(temp);
			Context.Entry(temp).Property(x => x.State).IsModified = true;
			Context.SaveChanges();
		}
	}
}
