using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Queries.Business;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<List<GetBusinessDTO>> GetBusiness([FromQuery] GetBusinessQuery query)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            return await mediator.Send(query, cancellationToken);
        }
    }
}
