using FeatureToggle.API.Identity;
﻿using CsvHelper;
using System.Globalization;
using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Queries.Log;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class LogController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<PaginatedLogListDTO> GetLogs(
            [FromQuery] int page, 
            [FromQuery] int pageSize,
            [FromQuery] string? searchQuery
            )
        {
            GetLogQuery query = new()
            {
                Page = page,
                PageSize = pageSize,
                SearchQuery = searchQuery
            };
            return await mediator.Send(query);
        }


        [HttpGet("AllLogs")]
        public async Task<FileContentResult> GetAllLogs()
        {
            List<LogDTO> logs = await mediator.Send(new GetAllLogsQuery());

            using (MemoryStream memoryStream = new())
            {
                using (StreamWriter streamWriter = new(memoryStream))
                using (CsvWriter csvWriter = new(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(logs);
                    streamWriter.Flush();
                }

                return new FileContentResult(memoryStream.ToArray(), "text/csv");
            }
        }

    }
}
