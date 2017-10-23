using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PictureStorage.Contexts
{
    public interface IPictureContext
	{
		IQueryable<Entities.Picture> Pictures { get; }

		void Add(Entities.Picture picture);

		void Update(Entities.Picture picture);

		Entities.Picture Find(Guid? id);

		int SaveChanges();
	}
}
