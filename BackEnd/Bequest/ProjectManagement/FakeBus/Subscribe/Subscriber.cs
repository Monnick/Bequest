using System;
using System.Collections.Generic;
using System.Text;
using Project.Shared.Messages;

namespace FakeBus.Subscribe
{
	public class Subscriber : Bus, Contracts.ISubscriber
	{
		private Dictionary<Type, Action<Message>> _handlers;

		public Subscriber(Contracts.IDataLayerAccess dataLayer)
			: base(dataLayer)
		{ }

		public void Register<T>(Action<Message> handleMessage) where T : Message
		{
			if (_handlers.ContainsKey(typeof(T)))
				throw new InvalidOperationException("handle already registered");

			_handlers.Add(typeof(T), handleMessage);
		}
	}
}
