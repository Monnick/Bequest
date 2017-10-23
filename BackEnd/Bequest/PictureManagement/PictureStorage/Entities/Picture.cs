using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PictureStorage.Entities
{
    public class Picture
    {
		[Key]
		public Guid Id { get; set; }

		public Guid ProjectId { get; set; }
		
		public string FileName { get; set; }

		public byte[] Data { get; set; }

		public Picture()
		{
			Id = Guid.NewGuid();
			Data = new byte[0];
		}
	}
}
