using Microsoft.IdentityModel.Tokens;
using SharedService.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SharedService.Modules
{
    public abstract class AuthorizedModule : BasicModule
	{
		private AppSettings _settings;

		public AuthorizedModule(AppSettings settings)
			: base()
		{
			_settings = settings;
		}

		public AuthorizedModule(string modulePath, AppSettings settings)
			: base(modulePath)
		{
			_settings = settings;
		}

		protected bool isAuthenticated()
		{
			try
			{
				var token = Request.Headers.Authorization;
				var key = Encoding.ASCII.GetBytes(_settings.Secret);

				var tokenHandler = new JwtSecurityTokenHandler();

				var validationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					RequireExpirationTime = true,
					IssuerSigningKey = new SymmetricSecurityKey(key)
				};

				SecurityToken validatedToken;
				try
				{
					tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
				}
				catch (Exception)
				{
					return false;
				}

				if (validatedToken != null && validatedToken.ValidTo >= DateTime.UtcNow)
					return true;
				return false;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
