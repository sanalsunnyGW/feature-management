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
        //private readonly BusinessContext _businessContext = businessContext;

        public async Task<List<GetBusinessDTO>> Handle(GetEnabledBusinessQuery request, CancellationToken cancellationToken)
        {

            var result = await businessContext.Business
            .GroupJoin(
                businessContext.BusinessFeatureFlag.Where(bff => bff.FeatureId == request.FeatureId),
                b => b.BusinessId,
                bff => bff.BusinessId,
                (business, featureFlags) => new { Business = business, FeatureFlags = featureFlags })
            .SelectMany(
                bf => bf.FeatureFlags.DefaultIfEmpty(), // Perform LEFT JOIN
                (bf, featureFlag) => new
                {
                    bf.Business.BusinessId,
                    bf.Business.BusinessName,
                    IsEnabled = featureFlag.IsEnabled
                })
            .Where(x => x.IsEnabled == false || x.IsEnabled == null)
            .Select(x => new GetBusinessDTO
            {
                BusinessId = x.BusinessId,
                BusinessName = x.BusinessName
            })
            .ToListAsync(cancellationToken);


            return result;

        }
    }
}
