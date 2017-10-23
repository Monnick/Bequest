using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserStorage.Contexts;
using UserStorage.Exceptions;
using UserStorage.Validators;

namespace UserStorage.Services
{
    public class UserService : Contracts.IUserService
	{
		protected IUserContext Context { get; private set; }

		public UserService(IUserContext context)
		{
			Context = context;
		}

		public Models.User Create(Models.User user)
		{
			// validation
			if (Context.Users.Any(x => x.Login == user.Login))
				throw new InvalidOperationException("Login '" + user.Login + "' is already taken");

			var validator = new UserValidator();
			var results = validator.Validate(user);

			if (!results.IsValid)
				throw new ValidationException(results);

			byte[] passwordHash, passwordSalt;
			CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

			var entity = new Entities.User(user);

			entity.PasswordHash = passwordHash;
			entity.PasswordSalt = passwordSalt;

			Context.Add(entity);
			Context.SaveChanges();

			user.Id = entity.Id;
			return user;
		}

		public Models.User Update(Models.User user)
		{
			var entity = Context.Find(user.Id);

			if (entity == null)
				throw new UserNotFoundException(user.Name);

			if (!string.IsNullOrEmpty(user.Password) && string.IsNullOrEmpty(user.OldPassword))
				throw new WrongPasswordException("Old password is required");

			var validator = new UserValidator();
			var results = validator.Validate(user);

			if (!results.IsValid)
				throw new ValidationException(results);

			// update user properties
			entity.Update(user);

			// update password if it was entered
			if (!string.IsNullOrWhiteSpace(user.Password))
			{
				if (!VerifyPasswordHash(user.OldPassword, entity.PasswordHash, entity.PasswordSalt))
					throw new WrongPasswordException("Old password is not correct");

				byte[] passwordHash, passwordSalt;
				CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

				entity.PasswordHash = passwordHash;
				entity.PasswordSalt = passwordSalt;
			}

			Context.Update(entity);
			Context.SaveChanges();

			user.Password = string.Empty;
			user.OldPassword = string.Empty;

			return user;
		}

		public Models.User Authenticate(string login, string password)
		{
			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
				return null;

			var entity = Context.Users.SingleOrDefault(x => x.Login == login);

			// check if username exists
			if (entity == null)
				throw new UserNotFoundException(login);

			// check if password is correct
			if (!VerifyPasswordHash(password, entity.PasswordHash, entity.PasswordSalt))
				throw new WrongPasswordException("Password is not correct");

			// authentication successful
			return entity.Convert();
		}

		public Models.User GetById(Guid id)
		{
			return Context.Users.SingleOrDefault(u => u.Id == id)?.Convert();
		}

		private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
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

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
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
