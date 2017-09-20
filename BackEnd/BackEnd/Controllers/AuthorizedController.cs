using AutoMapper;
using BackEnd.Configuration;
using BackEnd.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    public abstract class AuthorizedController<T> : BasicController<T> where T : IBasicService
	{
		protected AuthorizedController(
			T accountsService,
			AppSettings appSettings)
			: base(accountsService, appSettings)
		{
		}

		protected Guid? IsAuthorized()
		{
			Guid result;
			if (Guid.TryParse(User.Identity.Name, out result))
				return result;
			return null;
		}

		public IActionResult SecuredCall(Action<Guid> action)
		{
			try
			{
				var accountId = IsAuthorized();

				if (accountId == null)
					return Unauthorized();

				action(accountId.Value);

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		public IActionResult SecuredCall(Func<Guid, object> func)
		{
			try
			{
				var accountId = IsAuthorized();

				if (accountId == null)
					return Unauthorized();

				return Ok(func(accountId.Value));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
