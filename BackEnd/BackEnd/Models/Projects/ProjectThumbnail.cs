using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models.Projects
{
    public class ProjectThumbnail
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		public string Category { get; set; }

		public string Country { get; set; }
		
		public int Views { get; set; }
	}
}
