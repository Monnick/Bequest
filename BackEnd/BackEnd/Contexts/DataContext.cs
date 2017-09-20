using BackEnd.Entities.Projects;
using BackEnd.Entities.Accounts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Contexts.Contracts;

namespace BackEnd.Contexts
{
    public class DataContext : DbContext, IContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }
		
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<NeededItem> NeededItems { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Content> Content { get; set; }
		public DbSet<Picture> Pictures { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}

			base.OnModelCreating(modelBuilder);
		}
	}
}
