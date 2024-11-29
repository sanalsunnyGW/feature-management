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
        [HttpPost("feature/update")]
        public async Task<int> UpdateFeature(UpdateToggleCommand command) 
        {
            return await mediator.Send(command);
        }
    }

}
