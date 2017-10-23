using Project.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeBus.Contracts
{
    public interface IPublisher : IFakeBus
    {
		void Send<T>(T message) where T : Message;
	}
}
