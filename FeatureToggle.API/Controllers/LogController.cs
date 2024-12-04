using FeatureToggle.API.Identity;
using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Queries.Log;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class LogController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<PaginatedLogListDTO> GetLogs([FromQuery] GetLogQuery query, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(query,cancellationToken);
        }


        [HttpGet("download-logs")]
        public async Task<FileContentResult> DownloadLogs(CancellationToken cancellationToken = default)
        {
            return await mediator.Send(new DownloadLogsQuery(),cancellationToken);
        }

    }
}
