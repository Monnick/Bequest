using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Entities.Projects
{
    public class NeededItem
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		public Guid ProjectId { get; set; }

		[ForeignKey("ProjectId")]
		public Project Project { get; set; }

		public int? Quantity { get; set; }

		public int? Needed { get; set; }

		[Required]
		public string Name { get; set; }

		public NeededItem()
		{
			Id = Guid.NewGuid();
		}
	}
}
