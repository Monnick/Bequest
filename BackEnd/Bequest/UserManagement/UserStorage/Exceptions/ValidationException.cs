using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserStorage.Exceptions
{
	public class ValidationException : Exception
	{
		public ValidationResult Result { get; private set; }

		public ValidationException(ValidationResult result)
			: base()
		{
			Result = result;
		}
	}
}
