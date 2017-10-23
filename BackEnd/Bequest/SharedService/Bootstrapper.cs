using Nancy;
using Nancy.Configuration;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService
{
    public class Bootstrapper : DefaultNancyBootstrapper
	{
		private readonly IServiceProvider _serviceProvider;
		private static IList<Tuple<Type, object>> Singletons = new List<Tuple<Type, object>>();
		private static IList<Tuple<Type, object>> Transactions = new List<Tuple<Type, object>>();

		public Bootstrapper(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public override void Configure(INancyEnvironment environment)
		{
			environment.Tracing(true, true);
		}

		protected override void ConfigureApplicationContainer(TinyIoCContainer container)
		{
			base.ConfigureApplicationContainer(container);

			foreach (var registration in Singletons)
			{
				if (registration.Item2 == null)
					container.Register(registration.Item1);
				else
					container.Register(registration.Item1, registration.Item2);
			}
		}

		protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
		{
			base.ConfigureRequestContainer(container, context);

			foreach (var registration in Singletons)
			{
				if (registration.Item2 == null)
					container.Register(registration.Item1);
				else
					container.Register(registration.Item1, registration.Item2);
			}
		}

		public static void RegisterSingleton<TRegister>() where TRegister : class
		{
			RegisterSingleton(typeof(TRegister));
		}

		public static void RegisterSingleton<TRegister>(TRegister instance) where TRegister : class
		{
			RegisterSingleton(typeof(TRegister), instance);
		}

		public static void RegisterSingleton(Type type)
		{
			RegisterSingleton(type, null);
		}

		public static void RegisterSingleton(Type type, object instance)
		{
			Singletons.Add(new Tuple<Type, object>(type, instance));
		}

		public static void RegisterTransaction<TRegister>() where TRegister : class
		{
			RegisterTransaction(typeof(TRegister));
		}

		public static void RegisterTransaction<TRegister>(TRegister instance) where TRegister : class
		{
			RegisterTransaction(typeof(TRegister), instance);
		}

		public static void RegisterTransaction(Type type)
		{
			RegisterTransaction(type, null);
		}

		public static void RegisterTransaction(Type type, object instance)
		{
			Transactions.Add(new Tuple<Type, object>(type, instance));
		}
	}
}
