using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models.Projects
{
    public class Project
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		public Contact ContactData { get; set; }

		public string Category { get; set; }
		
		public State State { get; set; }

		public IEnumerable<State> PossibleStates { get; set; }

		public int Views { get; set; }
		
		public DateTime UpdatedAt { get; set; }
	}
}
