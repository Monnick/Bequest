using Microsoft.IdentityModel.Tokens;
using Nancy;
using Nancy.ModelBinding;
using SharedService.Configuration;
using SharedService.Modules;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using UserStorage.Exceptions;
using UserStorage.Models;
using UserStorage.Services.Contracts;

namespace UserService.Modules
{
    public class UserModule : AuthorizedModule
	{
		private IUserService _service;
		private AppSettings _settings;

		public UserModule(IUserService service, AppSettings settings)
			: base("/user", settings)
		{
			_service = service;
			_settings = settings;

			Get("/{id:guid}", args => { if (args == null) return (Response)HttpStatusCode.BadGateway; return Read(args.id); });
			Post("register", args => Create());
			Post("authenticate", args => Authenticate());
			Put("update", args => Update());
		}

		private dynamic Authenticate()
		{
			try
			{
				var user = this.Bind<User>();

				if (user == null)
					return BadRequest();

				var authUser = _service.Authenticate(user.Login, user.Password);

				if (authUser == null)
					return NotFound();

				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes(_settings.Secret);
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new Claim[]
					{
					new Claim(ClaimTypes.Name, authUser.Id.ToString())
					}),
					Expires = DateTime.UtcNow.AddDays(1),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				var tokenString = tokenHandler.WriteToken(token);

				// return basic user info (without password) and token to store client side
				return new
				{
					Id = authUser.Id,
					Token = tokenString
				};
			}
			catch (UserNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (WrongPasswordException ex)
			{
				return Forbidden(ex.Message);
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}

		private dynamic Create()
		{
			try
			{
				User dto = this.Bind<User>();

				if (dto == null)
					return BadRequest();

				try
				{
					return _service.Create(dto);
				}
				catch (InvalidOperationException ex)
				{
					return Conflict(ex.Message);
				}
				catch (ValidationException ex)
				{
					return Conflict(string.Join(';', ex.Result.Errors.Select(e => e.ErrorMessage)));
				}
			}
			catch (Exception ex)
			{
				return InternalServerError(ex.Message);
			}
		}

		private dynamic Read(Guid id)
		{
			try
			{
				User result = _service.GetById(id);

				if (result == null)
					return NotFound();

				return result;
			}
			catch (Exception ex)
			{
				return InternalServerError(ex.Message);
			}
		}

		private dynamic Update()
		{
			try
			{
				if (!isAuthenticated())
					return Unauthorized();

				User dto = this.Bind<User>();

				if (dto == null)
					return BadRequest();

				try
				{
					return _service.Update(dto);
				}
				catch (UserNotFoundException ex)
				{
					return NotFound(ex.Message);
				}
				catch (WrongPasswordException ex)
				{
					return Forbidden(ex.Message);
				}
				catch (ValidationException ex)
				{
					return Conflict(string.Join(';', ex.Result.Errors.Select(e => e.ErrorMessage)));
				}
			}
			catch (Exception ex)
			{
				return InternalServerError(ex.Message);
			}
		}
	}
}
