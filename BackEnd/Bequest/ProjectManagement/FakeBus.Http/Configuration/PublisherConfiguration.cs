using FakeBus.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeBus.Http.Configuration
{
    public class PublisherConfiguration : IPublisherConfiguration
    {
		public string TargetUrl { get; set; }

		public int Port { get; set; }
    }
}
