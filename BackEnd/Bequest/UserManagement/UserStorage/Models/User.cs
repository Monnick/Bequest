using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserStorage.Models
{
	[Serializable]
	public class User
	{
		public Guid? Id { get; set; }

		public string Name { get; set; }

		public string Phone { get; set; }

		public string Email { get; set; }

		public string Street { get; set; }

		public string City { get; set; }

		public string Zip { get; set; }

		public string Country { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public string OldPassword { get; set; }

		public string Json()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
