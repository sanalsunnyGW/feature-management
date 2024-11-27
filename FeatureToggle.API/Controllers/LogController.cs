using CsvHelper;
using System.Globalization;
using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Queries.Log;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
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
            GetLogQuery query = new GetLogQuery()
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

            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(logs);
                    streamWriter.Flush();
                }

                return new FileContentResult(memoryStream.ToArray(), "text/csv");
            }
        }

    }
}
