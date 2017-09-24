using BackEnd.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models.Projects;

namespace BackEnd.Services
{
    public class NeededItemsService : BasicService<DataContext>, Contracts.INeededItemsService
	{
		public NeededItemsService(DataContext context) : base(context)
		{
		}

		public NeededItems GetItems(Guid projectId)
		{
			if (!Context.Projects.Any(p => p.Id == projectId))
				throw new KeyNotFoundException("Project not found");

			var items = Context.NeededItems.Where(p => p.ProjectId == projectId).ToList();

			return new NeededItems
			{
				ProjectId = projectId,
				Items = items.Select(i => new NeededItem
				{
					Id = i.Id,
					Name = i.Name,
					Needed = i.Needed,
					Quantity = i.Quantity
				})
			};
		}

		public Guid UpdateItems(NeededItems dto)
		{
			if (!Context.Projects.Any(p => p.Id == dto.ProjectId))
				throw new KeyNotFoundException("Project not found");

			// delete items from db
			var guids = dto.Items.Select(d => d.Id).ToList();
			var toDelete = Context.NeededItems.Where(i => i.ProjectId == dto.ProjectId && !guids.Contains(i.Id));
			Context.NeededItems.RemoveRange(toDelete);

			// updated
			var entities = Context.NeededItems.Where(i => i.ProjectId == dto.ProjectId && guids.Contains(i.Id));

			foreach (var entity in entities)
			{
				var item = dto.Items.Single(i => i.Id == entity.Id);

				entity.Name = item.Name;
				entity.Needed = item.Needed;
				entity.Quantity = item.Quantity;

				Context.NeededItems.Update(entity);
			}

			// new items
			foreach (var item in dto.Items.Where(i => i.Id == null || i.Id == Guid.Empty))
			{
				Context.NeededItems.Add(new Entities.Projects.NeededItem
				{
					ProjectId = dto.ProjectId,
					Name = item.Name,
					Needed = item.Needed,
					Quantity = item.Quantity
				});
			}

			Context.SaveChanges();
			return dto.ProjectId;
		}
	}
}
