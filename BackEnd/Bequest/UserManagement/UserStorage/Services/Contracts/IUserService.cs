using System;
using System.Collections.Generic;
using System.Text;
using UserStorage.Models;

namespace UserStorage.Services.Contracts
{
    public interface IUserService
	{
		User Update(User user);

		User Create(User user);

		User Authenticate(string login, string password);

		User GetById(Guid id);
	}
}
