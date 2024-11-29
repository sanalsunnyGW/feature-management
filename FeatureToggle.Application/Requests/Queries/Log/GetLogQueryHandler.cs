using FeatureToggle.Application.DTOs;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Application.Requests.Queries.Log
{
    public class GetLogQueryHandler(FeatureManagementContext featureManagementContext, BusinessContext businessContext, UserManager<User> userManager) : IRequestHandler<GetLogQuery, PaginatedLogListDTO>
    {
        public async Task<PaginatedLogListDTO> Handle(GetLogQuery request, CancellationToken cancellationToken)
        {

            List<LogDTO> query = await featureManagementContext.Logs
                //.Where(x => request.SearchQuery == null || x.FeatureName.Contains(request.SearchQuery, StringComparison.OrdinalIgnoreCase))
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
         

            if (request.SearchQuery is not null)
            {
                query = query.Where(q => q.FeatureName.Contains(request.SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            int totalCount = query.Count();
            int page = request.Page;
            int pageSize = request.PageSize;
            int totalPages = (totalCount / pageSize) +1;
           

            List<LogDTO> queryList = query.Skip((page) * pageSize).Take(pageSize).ToList();


            PaginatedLogListDTO result = new PaginatedLogListDTO
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


