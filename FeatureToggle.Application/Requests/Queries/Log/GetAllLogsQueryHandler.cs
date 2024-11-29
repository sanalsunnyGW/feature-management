using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Application.DTOs;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;


using CsvHelper;
using System.IO;
using System.Globalization;
using System;

namespace FeatureToggle.Application.Requests.Queries.Log
{
    public class GetAllLogsQueryHandler(FeatureManagementContext featureManagementContext) : IRequestHandler<GetAllLogsQuery, List<LogDTO>>
    {
        public async Task<List<LogDTO>> Handle(GetAllLogsQuery request, CancellationToken cancellationToken)
        {
            List<LogDTO> query = await featureManagementContext.Logs
               .Select(x => new LogDTO
               {
                   LogId = x.Id,
                   UserId = x.UserId,
                   UserName = x.UserName,
                   FeatureId = x.FeatureId,
                   FeatureName = x.FeatureName,
                   BusinessId = x.BusinessId,
                   BusinessName = x.BusinessName,
                   Time = x.Time,
                   Action = x.Action,

               })
               .OrderByDescending(x => x.Time)
               .ToListAsync(cancellationToken);

            return query;

        }
    }
}
