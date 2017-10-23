using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Configuration
{
	public class AppSettings
	{
		public string Secret { get; set; }

		public AppSettings(string secret)
		{
			Secret = secret;
		}
	}
}
