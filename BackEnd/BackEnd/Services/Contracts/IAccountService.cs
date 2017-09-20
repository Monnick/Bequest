using BackEnd.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Contracts
{
    public interface IAccountService : IBasicService
    {
		void Update(Account accountParam, string password = null);
		
		Account Create(Account account, string password);

		Account Authenticate(string login, string password);

		Account GetById(Guid id);
	}
}
