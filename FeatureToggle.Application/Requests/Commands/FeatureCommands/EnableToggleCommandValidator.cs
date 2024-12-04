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
