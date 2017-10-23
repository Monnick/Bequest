using Nancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Modules
{
    public abstract class BasicModule : NancyModule
	{
		public BasicModule()
			: base()
		{ }

		public BasicModule(string modulePath)
			: base(modulePath)
		{ }

		protected Response Ok()
		{
			return (Response)HttpStatusCode.OK;
		}
		protected Response Ok(string message)
		{
			var response = (Response)message;
			response.StatusCode = HttpStatusCode.OK;
			return response;
		}

		protected Response Created()
		{
			return (Response)HttpStatusCode.Created;
		}
		protected object Created(string message)
		{
			var response = (Response)message;
			response.StatusCode = HttpStatusCode.Created;
			return response;
		}

		protected Response Conflict()
		{
			return (Response)HttpStatusCode.Conflict;
		}
		protected object Conflict(string message)
		{
			var response = (Response)message;
			response.StatusCode = HttpStatusCode.Conflict;
			return response;
		}

		protected Response BadRequest()
		{
			return (Response)HttpStatusCode.BadRequest;
		}
		protected object BadRequest(string message)
		{
			var response = (Response)message;
			response.StatusCode = HttpStatusCode.BadRequest;
			return response;
		}

		protected Response Unauthorized()
		{
			return (Response)HttpStatusCode.Unauthorized;
		}
		protected object Unauthorized(string message)
		{
			var response = (Response)message;
			response.StatusCode = HttpStatusCode.Unauthorized;
			return response;
		}

		protected Response Forbidden()
		{
			return (Response)HttpStatusCode.Forbidden;
		}
		protected object Forbidden(string message)
		{
			var response = (Response)message;
			response.StatusCode = HttpStatusCode.Forbidden;
			return response;
		}

		protected Response NotFound()
		{
			return (Response)HttpStatusCode.NotFound;
		}
		protected object NotFound(string message)
		{
			var response = (Response)message;
			response.StatusCode = HttpStatusCode.NotFound;
			return response;
		}

		protected Response InternalServerError()
		{
			return (Response)HttpStatusCode.InternalServerError;
		}
		protected object InternalServerError(string message)
		{
			var response = (Response)message;
			response.StatusCode = HttpStatusCode.InternalServerError;
			return response;
		}

		protected Response NotImplemented()
		{
			return (Response)HttpStatusCode.NotImplemented;
		}
		protected object NotImplemented(string message)
		{
			var response = (Response)message;
			response.StatusCode = HttpStatusCode.NotImplemented;
			return response;
		}

		protected Response BadGateway()
		{
			return (Response)HttpStatusCode.BadGateway;
		}
		protected object BadGateway(string message)
		{
			var response = (Response)message;
			response.StatusCode = HttpStatusCode.BadGateway;
			return response;
		}
	}
}
