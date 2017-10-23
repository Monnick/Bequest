using System;
using System.Collections.Generic;
using System.Text;

namespace UserStorage.Exceptions
{
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException(string userName)
			: base("User not found: " + userName)
		{ }
	}
}
