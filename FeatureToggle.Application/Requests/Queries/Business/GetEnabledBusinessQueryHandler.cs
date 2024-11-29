using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Application.DTOs;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FeatureToggle.Application.Requests.Queries.Business
{
    public class GetEnabledBusinessQueryHandler(BusinessContext businessContext) : IRequestHandler<GetEnabledBusinessQuery, List<GetBusinessDTO>>
    {
        public async Task<List<GetBusinessDTO>> Handle(GetEnabledBusinessQuery request, CancellationToken cancellationToken)
        {
            IQueryable<int?> businessesWithFlags = businessContext.BusinessFeatureFlag
                .Where(bff => bff.FeatureId == request.FeatureId && (bff.IsEnabled == false || bff.BusinessId == null))
                .Select(bff => bff.BusinessId);

            IQueryable<GetBusinessDTO> businessesWithoutFlags = businessContext.Business
                .Where(b => !businessContext.BusinessFeatureFlag
                    .Where(bff => bff.FeatureId == request.FeatureId)
                    .Select(bff => bff.BusinessId)
                    .Contains(b.BusinessId))
                .Select(b => new GetBusinessDTO
                {
                    BusinessId = b.BusinessId,
                    BusinessName = b.BusinessName
                });
             
            List<GetBusinessDTO> allBusinesses = await businessesWithoutFlags
                .Union(
                    businessContext.Business
                        .Where(b => businessesWithFlags.Contains(b.BusinessId))
                        .Select(b => new GetBusinessDTO
                        {
                            BusinessId = b.BusinessId,
                            BusinessName = b.BusinessName
                        })
                )
                .ToListAsync(cancellationToken);

            return allBusinesses;


        }
    }
}
