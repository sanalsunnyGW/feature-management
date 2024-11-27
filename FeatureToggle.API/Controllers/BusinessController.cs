using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Queries.Business;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController(IMediator mediator) : ControllerBase
    {
        [HttpGet("Enable")]

        public async Task<List<GetBusinessDTO>> GetEnabledFeature(
            [FromQuery] int featureId
            )
        {
            var query = new GetEnabledBusinessQuery
            {
                FeatureId = featureId
            };

            return await mediator.Send(query);  
        }

        [HttpGet("Disable")]

        public async Task<List<GetBusinessDTO>> GetDisableFeature( 
            [FromQuery] int featureId
            )
        {
            var query = new GetDisabledBusinessQuery
            {
                FeatureId = featureId
            };

            return await mediator.Send(query); 
        }
    }
}
