using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Application.DTOs;
using FeatureToggle.Domain.Entity.BusinessSchema;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Application.Requests.Queries.Business
{
    public class GetDisabledBusinessQueryHandler(BusinessContext businessContext) : IRequestHandler<GetDisabledBusinessQuery, List<GetBusinessDTO>>
    {
        public async Task<List<GetBusinessDTO>> Handle(GetDisabledBusinessQuery request, CancellationToken cancellationToken)
        {
            List<GetBusinessDTO> result = await businessContext.Business
            .Join(
                businessContext.BusinessFeatureFlag.Where(ff => ff.FeatureId == request.FeatureId && ff.IsEnabled == true),
                business => business.BusinessId,
                featureFlag => featureFlag.BusinessId,
                (business, featureFlag) => new GetBusinessDTO
                {
                    BusinessId = business.BusinessId,
                    BusinessName = business.BusinessName
                })
            .ToListAsync(cancellationToken);

            return result;

        }
    }
}
