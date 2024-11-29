using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Queries.Filter;
using FeatureToggle.Domain.Entity.BusinessSchema;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetFilteredFeaturesQueryHandler(BusinessContext businessContext) : IRequestHandler<GetFilteredFeaturesQuery, PaginatedFeatureListDTO>
{
    private readonly BusinessContext _businessContext = businessContext;
    private int pageSize = 12;

    public async Task<PaginatedFeatureListDTO> Handle(GetFilteredFeaturesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Feature> baseQuery = _businessContext.Feature.Include(f => f.BusinessFeatures);

        
        if (!request.FeatureToggleFilter.HasValue && !request.ReleaseToggleFilter.HasValue &&
            !request.IsEnabledFilter.HasValue && !request.IsDisabledFilter.HasValue)
        {
            List<FilteredFeatureDTO> allFeatures = await baseQuery.Select(f => new FilteredFeatureDTO
            {
                FeatureFlagId = f.BusinessFeatures != null && f.BusinessFeatures.Any()
                    ? f.BusinessFeatures.FirstOrDefault()!.FeatureFlagId
                    : 0,
                FeatureId = f.FeatureId,
                FeatureName = f.FeatureName,
                FeatureType = f.FeatureTypeId,
                isEnabled = f.BusinessFeatures != null && f.BusinessFeatures.Any()
                    ? f.BusinessFeatures.FirstOrDefault()!.IsEnabled
                    : null
            }).ToListAsync(cancellationToken);

            if(request.SearchQuery is not null)
            {
                allFeatures = allFeatures.Where(af => af.FeatureName.Contains(request.SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return new PaginatedFeatureListDTO
            {
                FeatureCount = allFeatures.Count,
                TotalPages = (allFeatures.Count + pageSize - 1) / pageSize,
                PageSize = pageSize,
                FeatureList = allFeatures.Skip(request.PageNumber * pageSize).Take(pageSize).ToList()
            };
        }

        if (request.FeatureToggleFilter.HasValue)
        {
            baseQuery = baseQuery.Where(f => f.FeatureTypeId == 2); 
        }

        if (request.ReleaseToggleFilter.HasValue)
        {
            baseQuery = baseQuery.Where(f => f.FeatureTypeId == 1); 

            if (request.IsEnabledFilter.HasValue && request.IsDisabledFilter.HasValue)
            {
                baseQuery = baseQuery.Where(f =>
                    f.BusinessFeatures!.Any(bf => bf.IsEnabled == true) ||   
                    f.BusinessFeatures!.Any(bf => !bf.IsEnabled) ||  
                    !f.BusinessFeatures!.Any()                      
                );
            }
            else if (request.IsEnabledFilter.HasValue)
            {
                baseQuery = baseQuery.Where(f =>
                    f.BusinessFeatures!.Any(bf => bf.IsEnabled == true)); 
            }
            else if (request.IsDisabledFilter.HasValue)
            {
                baseQuery = baseQuery.Where(f =>
                    f.BusinessFeatures!.Any(bf => !bf.IsEnabled) ||      
                    !f.BusinessFeatures!.Any());                        
            }
        }

        List<FilteredFeatureDTO> combinedQuery = await baseQuery.Select(f => new FilteredFeatureDTO
        {
            FeatureFlagId = f.BusinessFeatures != null && f.BusinessFeatures.Any()
                ? f.BusinessFeatures.First().FeatureFlagId
                : 0,
            FeatureId = f.FeatureId,
            FeatureName = f.FeatureName,
            FeatureType = f.FeatureTypeId,
            isEnabled = f.BusinessFeatures != null && f.BusinessFeatures.Any()
                ? f.BusinessFeatures.First().IsEnabled
                : null
        }).ToListAsync(cancellationToken);

        if (request.SearchQuery is not null)
        {
            combinedQuery = combinedQuery.Where(af => af.FeatureName.Contains(request.SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        List<FilteredFeatureDTO> featureList = combinedQuery
            .Skip(request.PageNumber * pageSize)
            .Take(pageSize)
            .ToList();

        int totalCount = combinedQuery.Count;

        return new PaginatedFeatureListDTO
        {
            FeatureCount = totalCount,
            TotalPages = (totalCount + pageSize - 1) / pageSize,
            PageSize = pageSize,
            FeatureList = featureList
        };
    }
}