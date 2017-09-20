using BackEnd.Entities.Projects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Contexts.Contracts
{
    public interface IContext
    {
		DbSet<Country> Countries { get; set; }
		DbSet<Category> Categories { get; set; }
	}
}
