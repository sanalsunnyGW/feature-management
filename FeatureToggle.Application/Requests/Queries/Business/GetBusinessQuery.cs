using FeatureToggle.Application.DTOs;
using MediatR;

namespace FeatureToggle.Application.Requests.Queries.Business
{
    public class GetBusinessQuery : IRequest<List<GetBusinessDTO>>
    {
        public int FeatureId { get; set; }
        public bool FeatureStatus { get; set; } 
    }
}
