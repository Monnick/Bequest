using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureStorage.Services.Contracts
{
    public interface IPictureService
    {
		byte[] GetPicture(Guid projectId);

		Guid UpdatePicture(Guid projectId, Stream pictureData);
	}
}
