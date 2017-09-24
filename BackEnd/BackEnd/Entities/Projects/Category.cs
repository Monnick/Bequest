using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Entities.Projects
{
    public class Category
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		public string Title { get; set; }
		
		public ICollection<Project> Projects { get; set; }

		public Category()
		{
			Id = Guid.NewGuid();
		}
	}
}
