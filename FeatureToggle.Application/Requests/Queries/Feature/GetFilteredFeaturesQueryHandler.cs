using FeatureToggle.Application.DTOs;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Application.Requests.Queries.Feature
{
    public class GetFilteredFeaturesQueryHandler(BusinessContext businessContext) : IRequestHandler<GetFilteredFeaturesQuery, PaginatedFeatureListDTO>
    {
        private readonly BusinessContext _businessContext = businessContext;
        private readonly int pageSize = 12;

        public async Task<PaginatedFeatureListDTO> Handle(GetFilteredFeaturesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Entity.BusinessSchema.Feature> baseQuery = _businessContext.Feature;


            if (!request.FeatureToggleFilter.HasValue && !request.ReleaseToggleFilter.HasValue &&
                !request.EnabledFilter.HasValue && !request.DisabledFilter.HasValue)
            {
                IQueryable<FilteredFeatureDTO> allFeatures = baseQuery.Select(f => new FilteredFeatureDTO
                {

                    FeatureId = f.FeatureId,
                    FeatureName = f.FeatureName,
                    FeatureType = f.FeatureTypeId,
                    IsEnabled = f.BusinessFeatures != null && f.BusinessFeatures.Count != 0
                        ? f.BusinessFeatures.FirstOrDefault()!.IsEnabled
                        : null
                });

                if (request.SearchQuery is not null)
                {
                    string searchQuery = request.SearchQuery.ToLower();
                    allFeatures = allFeatures.Where(af => EF.Functions.Like(af.FeatureName, $"%{searchQuery}%"));
                }

                List<FilteredFeatureDTO> allFeaturesList = await allFeatures.Skip(request.PageNumber * pageSize).Take(pageSize).ToListAsync(cancellationToken);

                return new PaginatedFeatureListDTO
                {
                    FeatureCount = allFeatures.Count(),
                    TotalPages = (allFeatures.Count() + pageSize - 1) / pageSize,
                    PageSize = pageSize,
                    FeatureList = allFeaturesList
                };
            }

            if (request.FeatureToggleFilter == true)
            {
                baseQuery = baseQuery.Where(f => f.FeatureTypeId == 2);
            }

            if (request.ReleaseToggleFilter == true)
            {
                baseQuery = baseQuery.Where(f => f.FeatureTypeId == 1);

                if (request.EnabledFilter == true && request.DisabledFilter == true)
                {
                    baseQuery = baseQuery.Where(f =>
                        f.BusinessFeatures!.Any(bf => bf.IsEnabled == true) ||
                        f.BusinessFeatures!.Any(bf => !bf.IsEnabled) ||
                        f.BusinessFeatures!.Count == 0
                    );
                }
                else if (request.EnabledFilter == true)
                {
                    baseQuery = baseQuery.Where(f =>
                        f.BusinessFeatures!.Any(bf => bf.IsEnabled == true));
                }
                else if (request.DisabledFilter == true)
                {
                    baseQuery = baseQuery.Where(f =>
                        f.BusinessFeatures!.Any(bf => !bf.IsEnabled) ||
                        f.BusinessFeatures!.Count == 0);
                }
            }

            IQueryable<FilteredFeatureDTO> combinedQuery = baseQuery.Select(f => new FilteredFeatureDTO
            {

                FeatureId = f.FeatureId,
                FeatureName = f.FeatureName,
                FeatureType = f.FeatureTypeId,
                IsEnabled = f.BusinessFeatures != null && f.BusinessFeatures.Count != 0
                    ? f.BusinessFeatures.First().IsEnabled
                    : null
            });

            if (request.SearchQuery is not null)
            {
                string searchQuery = request.SearchQuery.ToLower();
                combinedQuery = combinedQuery.Where(af => EF.Functions.Like(af.FeatureName, $"%{searchQuery}%"));
            }

            List<FilteredFeatureDTO> featureList = await combinedQuery
                .Skip(request.PageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            int totalCount = combinedQuery.Count();

            return new PaginatedFeatureListDTO
            {
                FeatureCount = totalCount,
                TotalPages = (totalCount + pageSize - 1) / pageSize,
                PageSize = pageSize,
                FeatureList = featureList
            };
        }
    }
}