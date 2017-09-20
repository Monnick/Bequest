using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models.Projects
{
    public class NeededItem
    {
		public Guid? Id { get; set; }

		public int Quantity { get; set; }

		public int Needed { get; set; }
		
		public string Name { get; set; }
	}
}
