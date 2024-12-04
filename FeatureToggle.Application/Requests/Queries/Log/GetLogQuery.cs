using FeatureToggle.Application.DTOs;
using MediatR;

namespace FeatureToggle.Application.Requests.Queries.Log
{
    public class GetLogQuery : IRequest<PaginatedLogListDTO>
    {
          public int Page { get; set; }
  
          public int PageSize { get; set; }

          public string? SearchQuery { get; set; }
    }
}
