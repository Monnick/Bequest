using FakeBus.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeBus.Http.Configuration
{
    public class SubscriberConfiguration : ISubscriberConfiguration
    {
		public int ListeningPort { get; set; }
    }
}
