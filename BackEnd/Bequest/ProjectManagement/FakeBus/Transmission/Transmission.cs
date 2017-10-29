using Project.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeBus.Transmission
{
	[Serializable]
    public class Transmission
	{
		public Type MessageType { get; set; }

		public string Message { get; set; }
	}
}
