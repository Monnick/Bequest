using BackEnd.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Models.Projects;
using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using BackEnd.Entities;

namespace BackEnd.Services
{
	public class ProjectsService : BasicService<DataContext>, Contracts.IProjectsService
	{
		public ProjectsService(DataContext context) : base(context)
		{
		}

		private Entities.Projects.Category GetCategory(string category)
		{
			var result = Context.Categories.FirstOrDefault(c => c.Title == category);

			if (result != null)
				return result;

			return new Entities.Projects.Category
			{
				Title = category
			};
		}

		public Guid CreateProject(Guid accountId, Project project)
		{
			DateTime now = DateTime.UtcNow;
			project.State = (int)Entities.State.New;

			Entities.Projects.Project entity = new Entities.Projects.Project
			{
				Category = GetCategory(project.Category),
				CreatedAt = now,
				Title = project.Title,
				InitiatorId = accountId,
				State = Entities.State.New,
				Views = 0
			};

			UpdateProject(ref entity, accountId, project);

			Context.Projects.Add(entity);
			Context.SaveChanges();

			return entity.Id;
		}

		public void DeleteProject(Guid accountId, Guid projectId)
		{
			if (!Context.Projects.Any(p => p.Id == projectId && p.InitiatorId == accountId))
				throw new AuthenticationException();

			Entities.Projects.Project entity = new Entities.Projects.Project() { Id = projectId };
			Context.Projects.Attach(entity);
			Context.Projects.Remove(entity);
			Context.SaveChanges();
		}

		public Project GetProject(Guid projectId)
		{
			var entity = Context.Projects.Include(p => p.Category).Include(p => p.Country).Single(p => p.Id == projectId);

			var result = new Project
			{
				Category = entity.Category?.Title,
				Id = entity.Id,
				State = entity.State,
				Title = entity.Title,
				UpdatedAt = entity.UpdatedAt,
				Views = entity.Views,
				PossibleStates = entity.State.GetPossibleStates(),
				ContactData = new Models.Contact
				{
					Name = entity.ContactName,
					Email = entity.Email,
					Phone = entity.Phone,
					Street = entity.Street,
					City = entity.City,
					Zip = entity.Zip,
					Country = entity.Country.Name
				}
			};

			return result;
		}

		public Guid UpdateProject(Guid accountId, Project project)
		{
			var entity = Context.Projects.Include(p => p.Category).Include(p => p.Country).Single(p => p.Id == project.Id && p.InitiatorId == accountId);

			UpdateProject(ref entity, entity.InitiatorId, project);

			Context.Projects.Update(entity);
			Context.SaveChanges();

			return entity.Id;
		}
		
		private void UpdateProject(ref Entities.Projects.Project entity, Guid accountId, Project project)
		{
			entity.UpdatedAt = DateTime.UtcNow;
			entity.State = project.State;
			entity.ContactName = project.ContactData.Name;
			entity.Email = project.ContactData.Email;
			entity.Phone = project.ContactData.Phone;
			entity.Street = project.ContactData.Street;
			entity.City = project.ContactData.City;
			entity.Zip = project.ContactData.Zip;
			entity.Country = Context.Countries.FirstOrDefault(c => c.Name == project.ContactData.Country);
		}
	}
}
