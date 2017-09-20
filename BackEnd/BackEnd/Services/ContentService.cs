using BackEnd.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models.Projects;

namespace BackEnd.Services
{
    public class ContentService : BasicService<DataContext>, Contracts.IContentService
	{
		public ContentService(DataContext context) : base(context)
		{
		}

		public Content GetContent(Guid projectId)
		{
			if (!Context.Projects.Any(p => p.Id == projectId))
				throw new KeyNotFoundException("Project not found");

			var entity = Context.Content.SingleOrDefault(c => c.ProjectId == projectId);

			if (entity == null)
			{
				entity = new Entities.Projects.Content() { ProjectId = projectId };
				Context.Content.Add(entity);
				Context.SaveChanges();
			}

			return new Content
			{
				Data = entity.Data,
				ProjectId = projectId
			};
		}

		public Guid UpdateContent(Guid projectId, string content)
		{
			if (!Context.Projects.Any(p => p.Id == projectId))
				throw new KeyNotFoundException("Project not found");

			var entity = Context.Content.SingleOrDefault(c => c.ProjectId == projectId);
			bool newEntity = entity == null;

			if (newEntity)
				entity = new Entities.Projects.Content() { ProjectId = projectId };

			entity.Data = content;

			if (newEntity)
				Context.Content.Add(entity);
			else
				Context.Content.Update(entity);
			Context.SaveChanges();

			return projectId;
		}
	}
}
