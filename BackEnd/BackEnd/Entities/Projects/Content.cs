using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Entities.Projects
{
    public class Content
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		public Guid ProjectId { get; set; }

		[ForeignKey("ProjectId")]
		public Project Project { get; set; }

		public string Data { get; set; }

		public Content()
		{
			Id = Guid.NewGuid();
		}
	}
}
