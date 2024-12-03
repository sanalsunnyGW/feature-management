using FluentValidation;

namespace FeatureToggle.Application.Requests.Commands.LogCommands
{
    public class AddLogCommandValidator : AbstractValidator<AddLogCommand>
    {
        public AddLogCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.FeatureId).NotEmpty();
            RuleFor(x => x.FeatureName).NotEmpty();
            RuleFor(x => x.action).NotEmpty().IsInEnum();

        }
    }
}
