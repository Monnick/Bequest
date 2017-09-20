using BackEnd.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Contracts
{
    public interface IContentService : IBasicService
	{
		Content GetContent(Guid projectId);

		Guid UpdateContent(Guid projectId, string content);
    }
}
