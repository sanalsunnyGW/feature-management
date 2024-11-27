using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FeatureToggle.Application.DTOs
{
    public class PaginatedLogListDTO
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<LogDTO> Logs { get; set; } = [];
    }
}
