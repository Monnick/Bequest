using AutoMapper;
using BackEnd.Configuration;
using BackEnd.Models.Accounts;
using BackEnd.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
	[EnableCors("SiteCorsPolicy")]
	[Authorize]
	[Route("Accounts")]
    public class AccountsController : BasicController<IAccountService>
	{
		public AccountsController(
			IAccountService accountsService,
			IOptions<AppSettings> appSettings)
			: base(accountsService, appSettings.Value)
		{
		}

		[AllowAnonymous]
		[HttpPost("authenticate")]
		public IActionResult Authenticate([FromBody]Account accountParam)
		{
			var account = Service.Authenticate(accountParam.Login, accountParam.Password);

			if (account == null)
				return Unauthorized();

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, account.Id.ToString())
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			// return basic user info (without password) and token to store client side
			return Ok(new
			{
				Id = account.Id,
				Login = account.Login,
				Name = account.Name,
				Token = tokenString
			});
		}

		[AllowAnonymous]
		[HttpPost("register")]
		public IActionResult Register([FromBody]Account account)
		{
			try
			{
				// save 
				return Ok(Service.Create(account, account.Password));
			}
			catch (Exception ex)
			{
				// return error message if there was an exception
				return BadRequest(ex.Message);
			}
		}
		
		[HttpGet("{id}")]
		public IActionResult GetById(Guid id)
		{
			if (string.IsNullOrEmpty(User.Identity.Name) || Guid.Parse(User.Identity.Name) != id)
				return Unauthorized();

			return Ok(Service.GetById(id));
		}

		[HttpPut]
		public IActionResult Update([FromBody]Account account)
		{
			try
			{
				if (string.IsNullOrEmpty(User.Identity.Name) || Guid.Parse(User.Identity.Name) != account.Id)
					return Unauthorized();
				
				// save 
				Service.Update(account, account.Password);
				return Ok(Service.GetById(account.Id));
			}
			catch (Exception ex)
			{
				// return error message if there was an exception
				return BadRequest(ex.Message);
			}
		}
	}
}
