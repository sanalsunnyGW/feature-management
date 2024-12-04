using System.Globalization;
using CsvHelper;
using FeatureToggle.Application.DTOs;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Application.Requests.Queries.Log
{
    public class DownloadLogsQueryHandler(FeatureManagementContext featureManagementContext) : IRequestHandler<DownloadLogsQuery, FileContentResult>
    {
        public async Task<FileContentResult> Handle(DownloadLogsQuery request, CancellationToken cancellationToken)
        {
            List<LogDTO> query = await featureManagementContext.Logs.Include(u => u.User)
               .Select(x => new LogDTO
               {
                   LogId = x.Id,
                   UserName = x.User.UserName,
                   FeatureId = x.FeatureId,
                   FeatureName = x.FeatureName,
                   BusinessId = x.BusinessId,
                   BusinessName = x.BusinessName,
                   Time = x.Time,
                   Action = x.Action,

               })
               .OrderByDescending(x => x.Time)
            .ToListAsync(cancellationToken);


            using MemoryStream memoryStream = new();
            using (StreamWriter streamWriter = new(memoryStream))
            using (CsvWriter csvWriter = new(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(query);
                streamWriter.Flush();
            }

            FileContentResult result = new(memoryStream.ToArray(), "text/csv");
            return result;


        }

    }
}
