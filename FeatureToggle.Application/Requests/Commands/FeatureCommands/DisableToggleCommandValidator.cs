using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FeatureToggle.Application.Requests.Commands.FeatureCommands
{
    public class DisableToggleCommandValidator : AbstractValidator<DisableToggleCommand>
    {
        public DisableToggleCommandValidator() 
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.FeatureId).NotEmpty();
        }
    }
}
