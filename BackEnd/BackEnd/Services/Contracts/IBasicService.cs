using BackEnd.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Contracts
{
    public interface IBasicService
    {
		IEnumerable<Models.Country> GetCountries();

		IEnumerable<Models.Category> GetCategories();
	}
}
