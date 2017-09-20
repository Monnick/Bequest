using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Entities.Projects
{
    public class Picture
    {
		[Key]
		public Guid Id { get; set; }

		public Guid ProjectId { get; set; }

		[ForeignKey("ProjectId")]
		public Project Project { get; set; }

		public string FileName { get; set; }

		public byte[] Data { get; set; }

		public Picture()
		{
			Id = Guid.NewGuid();
			Data = new byte[0];
		}
	}
}
