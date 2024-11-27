using System.Threading;
using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<AddUserResponseDTO> AddUser(AddUserCommand command)
        {
            var cancellationToken = HttpContext.RequestAborted;
            return await _mediator.Send(command,cancellationToken);
        }
        
        
        
    }
}
