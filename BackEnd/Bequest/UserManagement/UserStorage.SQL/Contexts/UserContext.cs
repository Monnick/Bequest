using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserStorage.Contexts;
using UserStorage.Entities;

namespace UserStorage.SQL.Contexts
{
	public class UserContext : DbContext, IUserContext
	{
		protected string ConnectionString { get; set; }
		public IQueryable<User> Users
		{
			get { return _users; }
		}
		private DbSet<User> _users { get; set; }

		public UserContext(DbContextOptions options)
			: base(options)
		{
			ConnectionString = string.Empty;
		}

		public UserContext(string connectionString)
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

		public void Update(Entities.User user)
		{
			_users.Update(user);
		}

		public void Add(Entities.User user)
		{
			_users.Add(user);
		}

		public Entities.User Find(Guid? id)
		{
			return _users.Find(id);
		}
	}
}
