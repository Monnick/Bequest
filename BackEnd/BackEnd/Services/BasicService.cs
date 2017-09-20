using AutoMapper;
using BackEnd.Contexts;
using BackEnd.Contexts.Contracts;
using BackEnd.Entities.Accounts;
using BackEnd.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public abstract class BasicService<TContext> : IBasicService where TContext : IContext
	{
		protected TContext Context { get; private set; }

		public BasicService(TContext context)
		{
			Context = context;
		}

		public IEnumerable<Models.Country> GetCountries()
		{
			return Context.Countries.Select(c => new Models.Country { Name = c.Name, Code = c.Code });
		}

		public IEnumerable<Models.Category> GetCategories()
		{
			return Context.Categories.Select(c => new Models.Category { Title = c.Title });
		}

		protected bool IsValidEmail(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}
	}
}
