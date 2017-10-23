using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Commands.Project
{
    public class CreateProject : Command
    {
		public Guid AggregateId { get; }

		public string Title { get; }

		public string Category { get; }

		public string Creator { get; }

		public CreateProject(Guid aggregateId, string title, string category, string creator)
		{
			AggregateId = aggregateId;
			Title = title;
			Category = category;
			Creator = creator;
		}
	}
}
