using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<LoginResponseDTO> SignIn(GetAuthTokenQuery command, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(command,cancellationToken);
        }
    }
}
