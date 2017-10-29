using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeBus
{
    public abstract class BusConnector
    {
		protected ILogger Logger { get; private set; }

		public BusConnector(ILogger logger)
		{
			Logger = logger;
		}

		public abstract void Initialize();

		public abstract void Start();

		public abstract void Stop();
    }
}
