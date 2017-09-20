using BackEnd.Contexts;
using BackEnd.Models.Projects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class PictureService : BasicService<DataContext>, Contracts.IPictureService
	{
		public PictureService(DataContext context) : base(context)
		{
		}

		public byte[] GetPicture(Guid projectId)
		{
			if (!Context.Projects.Any(p => p.Id == projectId))
				throw new KeyNotFoundException("Project not found");

			var pic = Context.Pictures.SingleOrDefault(p => p.ProjectId == projectId);

			return pic != null ? pic.Data : new byte[0];
		}

		private static byte[] ReadFully(Stream input)
		{
			byte[] buffer = new byte[4 * 1024 * 1024]; // 4 MB
			using (MemoryStream ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}

		public Guid UpdatePicture(Guid projectId, Stream pictureData)
		{
			if (!Context.Projects.Any(p => p.Id == projectId))
				throw new KeyNotFoundException("Project not found");

			var entity = Context.Pictures.SingleOrDefault(c => c.ProjectId == projectId);
			bool newEntity = entity == null;

			if (newEntity)
				entity = new Entities.Projects.Picture() { ProjectId = projectId };
			
			entity.Data = ReadFully(pictureData);

			if (newEntity)
				Context.Pictures.Add(entity);
			else
				Context.Pictures.Update(entity);
			Context.SaveChanges();

			return projectId;
		}
	}
}
