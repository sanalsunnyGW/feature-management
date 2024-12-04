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
