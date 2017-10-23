using System;
using System.Collections.Generic;
using System.Text;

namespace UserStorage.Exceptions
{
	public class WrongPasswordException : Exception
	{
		public WrongPasswordException()
			: base()
		{ }

		public WrongPasswordException(string message)
			: base(message)
		{ }

		public WrongPasswordException(string message, System.Exception innerException)
			: base(message, innerException)
		{ }
	}
}
