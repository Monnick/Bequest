using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Commands.Project.Validation
{
    public class CreateProjectValidator : AbstractValidator<CreateProject>
	{
		public CreateProjectValidator()
		{
			RuleFor(cmd => cmd.AggregateId).NotEqual(Guid.Empty).WithMessage("Please specify an id");
			RuleFor(cmd => cmd.Title).NotEmpty().WithMessage("Please specify a title");
			RuleFor(cmd => cmd.Category).NotEmpty().WithMessage("Please specify a category");
			RuleFor(cmd => cmd.Creator).NotEmpty().WithMessage("Please specify a creator");
		}
	}
}
