using BackEnd.Models.Projects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Contracts
{
    public interface IPictureService : IBasicService
	{
		byte[] GetPicture(Guid projectId);

		Guid UpdatePicture(Guid projectId, Stream pictureData);
	}
}
