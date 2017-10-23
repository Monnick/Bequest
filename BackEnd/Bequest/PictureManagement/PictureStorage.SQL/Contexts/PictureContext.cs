using Microsoft.EntityFrameworkCore;
using PictureStorage.Contexts;
using PictureStorage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PictureStorage.SQL.Contexts
{
	public class PictureContext : DbContext, IPictureContext
	{
		protected string ConnectionString { get; set; }
		public IQueryable<Picture> Pictures
		{
			get { return _pictures; }
		}
		private DbSet<Picture> _pictures { get; set; }

		public PictureContext(DbContextOptions options)
			: base(options)
		{
			ConnectionString = string.Empty;
		}

		public PictureContext(string connectionString)
			: base()
		{
			ConnectionString = connectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			if (!string.IsNullOrEmpty(ConnectionString))
				builder.UseSqlServer(ConnectionString);

			base.OnConfiguring(builder);
		}

		public void Update(Entities.Picture picture)
		{
			_pictures.Update(picture);
		}

		public void Add(Entities.Picture picture)
		{
			_pictures.Add(picture);
		}

		public Entities.Picture Find(Guid? id)
		{
			return _pictures.Find(id);
		}
	}
}
