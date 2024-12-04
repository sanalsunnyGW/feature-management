using MediatR;

namespace FeatureToggle.Application.Requests.Commands.FeatureCommands
{
    public class EnableToggleCommand : IRequest<int>
    {
        public required string UserId { get; set; }
        public int FeatureId { get; set; }

        public int? BusinessId { get; set; }
    }
}
