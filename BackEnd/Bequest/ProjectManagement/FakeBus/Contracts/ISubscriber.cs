using Project.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeBus.Contracts
{
    public interface ISubscriber : IFakeBus
	{
		void Register<T>(Action<Message> handleMessage) where T : Message;
	}
}
