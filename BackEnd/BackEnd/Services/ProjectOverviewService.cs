using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models.Projects;
using BackEnd.Contexts;
using AutoMapper;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services
{
	public class ProjectOverviewService : BasicService<DataContext>, Contracts.IProjectOverviewService
	{
		public ProjectOverviewService(DataContext context) : base(context) { }

		public IEnumerable<Guid> GetLatestProjects(int count, int offset)
		{
			return GetProjects(GetCurrentProjects(count, offset).OrderByDescending(p => p.UpdatedAt));
		}
		
		public IEnumerable<Guid> GetProjectsByCountry(int count, int offset, string country)
		{
			return GetProjects(GetCurrentProjects(count, offset).Where(p => p.Country.Name == country || p.Country.Code == country));
		}

		public IEnumerable<Guid> GetTopProjects(int count, int offset)
		{
			return GetProjects(GetCurrentProjects(count, offset).OrderByDescending(p => p.Views));
		}
		
		private IQueryable<Entities.Projects.Project> GetCurrentProjects(int count, int offset)
		{
			return Context.Projects.Include(c => c.Country).Include(c => c.Category);//.Where(p => p.State == Entities.State.Online).Take(count).Skip(offset);
		}

		private IEnumerable<Guid> GetProjects(IQueryable<Entities.Projects.Project> projects)
		{
			return projects.Select(p => p.Id);
		}

		public ProjectThumbnail Get(Guid projectId)
		{
			var entity = Context.Projects.Include(p => p.Category).Include(p => p.Country).Single(p => p.Id == projectId);

			return new ProjectThumbnail
			{
				Id = entity.Id,
				Title = entity.Title,
				Category = entity.Category == null ? string.Empty : entity.Category.Title,
				Country = entity.Country.Name,
				Views = entity.Views
			};
		}

		//private IEnumerable<ProjectThumbnail> GetProjects(IQueryable<Entities.Projects.Project> projects)
		//{
		//	return projects.Select(p => new ProjectThumbnail
		//	{
		//		Id = p.Id,
		//		Title = p.Title,
		//		Category = p.Category == null ? string.Empty : p.Category.Title,
		//		Country = p.Country.Name,
		//		Views = p.Views
		//	});
		//}
	}
}
