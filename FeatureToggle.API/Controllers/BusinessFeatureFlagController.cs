using FeatureToggle.API.Identity;
using FeatureToggle.Application.Requests.Commands.FeatureCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessFeatureFlagController(IMediator mediator) : ControllerBase
    {
        [HttpPost("toggle/enable")]
        public async Task<int> EnableFeature(EnableToggleCommand command, CancellationToken cancellationToken = default) 
        {
            return await mediator.Send(command, cancellationToken);
        }

        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpPost("toggle/disable")]
        public async Task<int> DisableFeature(DisableToggleCommand command, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(command, cancellationToken);
        }
    }

}
