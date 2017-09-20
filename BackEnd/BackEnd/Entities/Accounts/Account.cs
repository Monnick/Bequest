using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Entities.Accounts
{
    public class Account
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; }
		
		public string Phone { get; set; }

		[Required]
		public string Email { get; set; }

		public string Street { get; set; }
		
		public string City { get; set; }
		
		public string Zip { get; set; }

		[Required]
		public string Country { get; set; }
		
		[Required]
		public string Login { get; set; }

		[Required]
		public byte[] PasswordHash { get; set; }

		[Required]
		public byte[] PasswordSalt { get; set; }

		public ICollection<Projects.Project> Projects { get; set; }

		public Account()
		{
			Id = Guid.NewGuid();
		}
	}
}
