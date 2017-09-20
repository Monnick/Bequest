using BackEnd.Contexts;
using BackEnd.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Entities.Accounts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services
{
	public class AccountService : BasicService<DataContext>, IAccountService
	{
		public AccountService(DataContext context) : base(context) { }

		public Models.Accounts.Account Create(Models.Accounts.Account accountParam, string password)
		{
			// validation
			if (!IsValidEmail(accountParam.Email))
				throw new DataMisalignedException("Email is not valid");

			if (string.IsNullOrWhiteSpace(password))
				throw new DataMisalignedException("Password is required");

			if (Context.Accounts.Any(x => x.Login == accountParam.Login))
				throw new InvalidOperationException("Username " + accountParam.Login + " is already taken");

			byte[] passwordHash, passwordSalt;
			CreatePasswordHash(password, out passwordHash, out passwordSalt);
			
			var account = new Account
			{
				City = accountParam.City,
				Country = accountParam.Country,
				Email = accountParam.Email,
				Login = accountParam.Login,
				Name = accountParam.Name,
				Phone = accountParam.Phone,
				Street = accountParam.Street,
				Zip = accountParam.Zip
			};

			account.PasswordHash = passwordHash;
			account.PasswordSalt = passwordSalt;

			Context.Accounts.Add(account);
			Context.SaveChanges();
			
			return accountParam;
		}

		public Models.Accounts.Account GetById(Guid id)
		{
			var account = Context.Accounts.SingleOrDefault(a => a.Id == id);

			if (account == null)
				throw new KeyNotFoundException("Account not found");

			return new Models.Accounts.Account {
				City = account.City,
				Country = account.Country,
				Email = account.Email,
				Id = account.Id,
				Login = account.Login,
				Name = account.Name,
				Phone = account.Phone,
				Street = account.Street,
				Zip = account.Zip
			};
		}
		
		public void Update(Models.Accounts.Account accountParam, string password = null)
		{
			var account = Context.Accounts.Find(accountParam.Id);

			if (account == null)
				throw new KeyNotFoundException("Account not found");

			if (!string.IsNullOrEmpty(accountParam.Password) && string.IsNullOrEmpty(accountParam.OldPassword))
				throw new DataMisalignedException("Old password is required");

			if (accountParam.Login != account.Login)
			{
				// username has changed so check if the new username is already taken
				if (Context.Accounts.Any(x => x.Login == accountParam.Login))
					throw new DataMisalignedException("Login " + accountParam.Login + " is already taken");
			}

			// update user properties
			account.City = accountParam.City;
			account.Country = accountParam.Country;
			account.Email = accountParam.Email;
			account.Name = accountParam.Name;
			account.Phone = accountParam.Phone;
			account.Street = accountParam.Street;
			account.Zip = accountParam.Zip;

			// update password if it was entered
			if (!string.IsNullOrWhiteSpace(password))
			{
				if (!VerifyPasswordHash(accountParam.OldPassword, account.PasswordHash, account.PasswordSalt))
					throw new DataMisalignedException("Old password is not correct");

				byte[] passwordHash, passwordSalt;
				CreatePasswordHash(password, out passwordHash, out passwordSalt);

				account.PasswordHash = passwordHash;
				account.PasswordSalt = passwordSalt;
			}

			Context.Accounts.Update(account);
			Context.SaveChanges();
		}

		public Models.Accounts.Account Authenticate(string login, string password)
		{
			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
				return null;

			var account = Context.Accounts.SingleOrDefault(x => x.Login == login);

			// check if username exists
			if (account == null)
				return null;

			// check if password is correct
			if (!VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
				return null;

			// authentication successful
			return new Models.Accounts.Account {
				Id = account.Id,
				City = account.City,
				Country = account.Country,
				Email = account.Email,
				Name = account.Name,
				Login = account.Login,
				Phone = account.Phone,
				Street = account.Street,
				Zip = account.Zip
			};
		}

		private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
		{
			if (password == null) throw new ArgumentNullException("password");
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
			if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
			if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

			using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < computedHash.Length; i++)
				{
					if (computedHash[i] != storedHash[i]) return false;
				}
			}

			return true;
		}

		private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			if (password == null) throw new ArgumentNullException("password");
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}
	}
}
