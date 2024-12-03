using FeatureToggle.Application.DTOs;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Application.Requests.Queries.Log
{
    public class GetLogQueryHandler(FeatureManagementContext featureManagementContext) : IRequestHandler<GetLogQuery, PaginatedLogListDTO>
    {
        public async Task<PaginatedLogListDTO> Handle(GetLogQuery request, CancellationToken cancellationToken)
        {

            IQueryable<LogDTO> query = featureManagementContext.Logs
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
                .OrderByDescending(x => x.Time);
         

            if (request.SearchQuery is not null)
            {
                string searchQuery = request.SearchQuery.ToLower();
                query = query.Where(af => EF.Functions.Like(af.FeatureName, $"%{searchQuery}%"));
            }

            int totalCount = query.Count();
            int page = request.Page;
            int pageSize = request.PageSize;
            int totalPages = (totalCount / pageSize) +1;

            List<LogDTO> queryList = await query.Skip((page) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            PaginatedLogListDTO result = new()
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = request.Page,
                PageSize = request.PageSize,
                Logs = queryList
            };

            return result;

        }
    }
}


