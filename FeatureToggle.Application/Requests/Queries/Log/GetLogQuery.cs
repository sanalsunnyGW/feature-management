using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.Application.Requests.Queries.Log
{
    public class GetLogQuery : IRequest<PaginatedLogListDTO>
    {
          public int Page { get; set; }
  
          public int PageSize { get; set; }

          public string? SearchQuery { get; set; }
    }
}
