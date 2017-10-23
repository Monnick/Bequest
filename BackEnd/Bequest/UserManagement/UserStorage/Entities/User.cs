using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserStorage.Entities
{
    public class User
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

		public User()
			: base()
		{
		}
		public User(Models.User dto)
			: base()
		{
			Id = Guid.NewGuid();

			Login = dto.Login;

			Update(dto);
		}

		public void Update(Models.User dto)
		{
			if (dto.Id != null)
				Id = dto.Id.Value;

			Name = dto.Name;
			Phone = dto.Phone;
			Email = dto.Email;
			Street = dto.Street;
			City = dto.City;
			Zip = dto.Zip;
			Country = dto.Country;
		}

		public Models.User Convert()
		{
			return new Models.User
			{
				Id = this.Id,
				Name = this.Name,
				Phone = this.Phone,
				Email = this.Email,
				Street = this.Street,
				City = this.City,
				Zip = this.Zip,
				Country = this.Country,
				Login = this.Login
			};
		}
	}
}
