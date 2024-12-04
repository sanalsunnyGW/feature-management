using FeatureToggle.Application.DTOs;
using MediatR;

namespace FeatureToggle.Application.Requests.Queries.Feature
{
    public class GetFilteredFeaturesQuery : IRequest<PaginatedFeatureListDTO>
    {
        public int PageNumber { get; set; }
        public string? SearchQuery { get; set; }
        public bool? FeatureToggleFilter { get; set; }
        public bool? ReleaseToggleFilter { get; set; }
        public bool? EnabledFilter { get; set; } 
        public bool? DisabledFilter { get; set; }
                
    }
}
