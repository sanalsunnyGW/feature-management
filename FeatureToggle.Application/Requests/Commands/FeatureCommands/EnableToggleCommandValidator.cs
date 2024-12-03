using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Application.Requests.Commands.FeatureCommands;
using FluentValidation;

namespace FeatureToggle.Application.Requests.Commands.FeatureCommands
{
    public class EnableToggleCommandValidator : AbstractValidator<EnableToggleCommand>
    {
        public EnableToggleCommandValidator() 
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.FeatureId).NotEmpty();
        }
    }
}
