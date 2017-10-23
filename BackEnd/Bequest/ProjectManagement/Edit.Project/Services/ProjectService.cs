using FakeBus.Contracts;
using Project.Commands.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edit.Project.Services
{
    public class ProjectService
    {
		private IPublisher _publisher;

		public ProjectService(IPublisher publisher)
		{
			_publisher = publisher;
		}

		public void CreateProject(CreateProject cmd)
		{ }
    }
}
