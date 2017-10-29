using System;
using System.Collections.Generic;
using System.Text;
using Project.Shared.Messages;
using System.Threading.Tasks;
using FakeBus.Contracts;
using Microsoft.Extensions.Logging;

namespace FakeBus.Subscribe
{
	public abstract class Subscriber : BusConnector
	{
		private Dictionary<Type, Action<Message>> _handlers;

		protected ISubscriberConfiguration Configuration { get; private set; }

		public Subscriber(ILogger logger, ISubscriberConfiguration config) : base(logger)
		{
			Configuration = config;
			_handlers = new Dictionary<Type, Action<Message>>();
		}

		public void Register<T>(Action<Message> handleMessage) where T : Message
		{
			if (_handlers.ContainsKey(typeof(T)))
				throw new InvalidOperationException("handle already registered");

			_handlers.Add(typeof(T), handleMessage);
		}

		protected void HandleMessage(Message message, Type t)
		{
			if (_handlers.ContainsKey(t))
				Task.Run(() => _handlers[t](message));
		}
	}
}
