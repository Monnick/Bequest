using BackEnd.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Contracts
{
    public interface INeededItemsService : IBasicService
	{
		NeededItems GetItems(Guid projectId);

		Guid UpdateItems(NeededItems items);
    }
}
