using BackEnd.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Entities.Projects
{
    public class Project
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string ContactName { get; set; }

		public string Street { get; set; }

		public string City { get; set; }

		public string Zip { get; set; }

		[Required]
		public Guid CountryId { get; set; }

		[ForeignKey("CountryId")]
		public Country Country { get; set; }
		
		public string Phone { get; set; }

		[Required]
		public string Email { get; set; }

		public Guid? CategoryId { get; set; }

		[ForeignKey("CategoryId")]
		public Category Category { get; set; }
		
		[Required]
		public Guid InitiatorId { get; set; }
		
		public ICollection<NeededItem> Items { get; set; }

		public Guid? ContentId { get; set; }

		public Guid? PictureId { get; set; }

		[Required]
		public State State { get; set; }

		[Required]
		public int Views { get; set; }

		[Required]
		public DateTime UpdatedAt { get; set; }
		
		[Required]
		public DateTime CreatedAt { get; set; }

		public Project()
		{
			Id = Guid.NewGuid();
		}
	}
}
