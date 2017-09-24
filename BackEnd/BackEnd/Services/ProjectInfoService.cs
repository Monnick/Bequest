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
		
		public IEnumerable<ProjectInfo> GetProjects(Guid accountId)
		{
			return GetProjectsByAccount(accountId).ToList().Select(p => new ProjectInfo
			{
				Id = p.Id,
				Title = p.Title,
				Category = p.Category.Title,
				UpdatedAt = p.UpdatedAt,
				State = p.State,
				PossibleStates = p.State.GetPossibleStates(),
				Views = p.Views,
				NeededItems = p.Items.Select(i => new NeededItem
				{
					Id = i.Id,
					Name = i.Name,
					Needed = i.Needed,
					Quantity = i.Quantity
				})
			});
		}

		private IQueryable<Entities.Projects.Project> GetProjectsByAccount(Guid accountId)
		{
			return Context.Projects.Where(p => p.InitiatorId == accountId).Include(c => c.Category).Include(c => c.Items);
		}

		public NeededItems UpdateNeededItems(NeededItems items)
		{
			var guids = items.Items.Select(i => i.Id).ToList();
			var entities = Context.NeededItems.Where(i => i.ProjectId == items.ProjectId && guids.Contains(i.Id));

			foreach (var entity in entities)
			{
				var item = items.Items.FirstOrDefault(i => i.Id == entity.Id);

				if (item == null)
					continue;

				entity.Needed = item.Needed;
				entity.Quantity = item.Quantity;
			}

			Context.NeededItems.UpdateRange(entities);
			Context.SaveChanges();

			return items;
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
