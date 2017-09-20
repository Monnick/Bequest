using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models.Projects
{
    public class Picture
	{
		public Guid ProjectId { get; set; }

		public string FileName { get; set; }

		public object Data { get; set; }
	}
}
