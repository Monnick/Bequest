using PictureStorage.Contexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PictureStorage.Services
{
	public class PictureService : Contracts.IPictureService
	{
		private IPictureContext _context;

		public PictureService(IPictureContext context)
		{
			_context = context;
		}

		public byte[] GetPicture(Guid projectId)
		{
			if (!_context.Pictures.Any(p => p.Id == projectId))
				throw new KeyNotFoundException("Project not found");

			var pic = _context.Pictures.SingleOrDefault(p => p.ProjectId == projectId);

			return pic != null ? pic.Data : new byte[0];
		}

		public Guid UpdatePicture(Guid projectId, Stream pictureData)
		{
			if (!_context.Pictures.Any(p => p.Id == projectId))
				throw new KeyNotFoundException("Project not found");

			var entity = _context.Pictures.SingleOrDefault(c => c.ProjectId == projectId);
			bool newEntity = entity == null;

			if (newEntity)
				entity = new Entities.Picture() { ProjectId = projectId };

			entity.Data = ReadFully(pictureData);

			if (newEntity)
				_context.Add(entity);
			else
				_context.Update(entity);
			_context.SaveChanges();

			return projectId;
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
	}
}
