using FluentValidation;

namespace FeatureToggle.Application.Requests.Commands.FeatureCommands
{
    public class UpdateToggleCommandValidator : AbstractValidator<UpdateToggleCommand>
    {
        public UpdateToggleCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.FeatureId).NotEmpty();
            RuleFor(x => x.EnableOrDisable).NotEmpty();
        }
    }
}
