﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Contact
	{
		public string Name { get; set; }
		
		public string Street { get; set; }

		public string City { get; set; }

		public string Zip { get; set; }

		public string Country { get; set; }

		public string Phone { get; set; }

		public string Email { get; set; }
	}
}
