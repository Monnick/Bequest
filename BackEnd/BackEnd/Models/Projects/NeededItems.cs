using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models.Projects
{
    public class NeededItems
    {
		public Guid ProjectId { get; set; }

		public IEnumerable<NeededItem> Items { get; set; }
    }
}
