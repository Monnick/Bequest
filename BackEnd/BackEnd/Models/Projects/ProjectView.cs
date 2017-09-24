using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models.Projects
{
    public class ProjectView
	{
		public string Title { get; set; }

		public Contact ContactData { get; set; }

		public string Category { get; set; }

		public string Content { get; set; }

		public IEnumerable<NeededItem> NeededItems { get; set; }
	}
}
