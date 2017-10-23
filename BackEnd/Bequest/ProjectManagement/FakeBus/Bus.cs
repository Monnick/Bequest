using System;
using System.Collections.Generic;
using System.Text;

namespace FakeBus
{
    public class Bus : Contracts.IFakeBus
    {
		protected Contracts.IDataLayerAccess DataLayerAccess { get; private set; }

		public Bus(Contracts.IDataLayerAccess dataLayer)
		{
			DataLayerAccess = dataLayer;
		}

		public void Initialize()
		{ }
	}
}
