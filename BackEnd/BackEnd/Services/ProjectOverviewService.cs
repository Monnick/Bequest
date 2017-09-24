using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models.Projects;
using BackEnd.Contexts;
using AutoMapper;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using BackEnd.Services.Contracts;

namespace BackEnd.Services
{
	public class ProjectOverviewService : BasicService<DataContext>, Contracts.IProjectOverviewService
	{
		public ProjectOverviewService(DataContext context) : base(context) { }

		public IEnumerable<ProjectThumbnail> GetProjects(int offset, int count, OrderBy order, string category, string country)
		{
			return FormatProjects(OrderProjects(FilterProjects(GetProjects(offset, count), category, country), order));
		}

		private IQueryable<Entities.Projects.Project> GetProjects(int offset, int count)
		{
			return Context.Projects.Include(c => c.Country).Include(c => c.Category).Take(count).Skip(offset);
		}

		private IQueryable<Entities.Projects.Project> OrderProjects(IQueryable<Entities.Projects.Project> projects, OrderBy order)
		{
			switch (order)
			{
				case OrderBy.Undefined:
					return projects;
				case OrderBy.Views:
					return projects.OrderByDescending(p => p.Views);
				case OrderBy.Updated:
					return projects.OrderByDescending(p => p.UpdatedAt);
				default:
					return projects;
			}
		}

		private IQueryable<Entities.Projects.Project> FilterProjects(IQueryable<Entities.Projects.Project> projects, string category, string country)
		{
			Guid? countryId = null;
			Guid? categoryId = null;

			if (!string.IsNullOrEmpty(country))
				countryId = Context.Countries.Where(c => c.Name == country).Select(c => c.Id).FirstOrDefault();

			if (!string.IsNullOrEmpty(category))
				categoryId = Context.Categories.Where(c => c.Title == category).Select(c => c.Id).FirstOrDefault();

			return projects.Where(p => (categoryId == null || p.CategoryId == categoryId) && (countryId == null || p.CountryId == countryId) && p.State == Entities.State.Online);
		}

		private IEnumerable<ProjectThumbnail> FormatProjects(IQueryable<Entities.Projects.Project> projects)
		{
			return projects.Select(p => new {
				Id = p.Id,
				Category = p.Category == null ? string.Empty : p.Category.Title,
				Initiator = p.ContactName,
				Title = p.Title,
				Country = p.Country == null ? string.Empty : p.Country.Name,
				Views = p.Views,
				UpdatedAt = p.UpdatedAt
			}
			).ToList().Select(p => new ProjectThumbnail
			{
				Id = p.Id,
				Category = p.Category,
				Initiator = p.Initiator,
				Title = p.Title,
				Country = p.Country,
				Views = p.Views,
				UpdatedAt = p.UpdatedAt
			});
		}

		public ProjectView Get(Guid projectId)
		{
			var entity = Context.Projects.Include(p => p.Category).Include(p => p.Country).Include(p => p.Items).Single(p => p.Id == projectId);
			entity.Views++;
			Context.Projects.Update(entity);
			Context.SaveChanges();

			var content = Context.Content.Single(c => c.ProjectId == projectId);

			var result = new ProjectView
			{
				Title = entity.Title,
				Category = entity.Category?.Title,
				NeededItems = entity.Items.Select(i => new NeededItem
				{
					Id = i.Id,
					Name = i.Name,
					Needed = i.Needed,
					Quantity = i.Quantity
				}),
				ContactData = new Models.Contact
				{
					Name = entity.ContactName,
					Email = entity.Email,
					Phone = entity.Phone,
					Street = entity.Street,
					City = entity.City,
					Zip = entity.Zip,
					Country = entity.Country.Name
				},
				Content = content.Data
			};

			return result;
		}

		private IEnumerable<ProjectThumbnail> GetProjects(IQueryable<Entities.Projects.Project> projects)
		{
			return projects.Select(p => new ProjectThumbnail
			{
				Id = p.Id,
				Title = p.Title,
				Category = p.Category == null ? string.Empty : p.Category.Title,
				Country = p.Country.Name
			});
		}
	}
}
