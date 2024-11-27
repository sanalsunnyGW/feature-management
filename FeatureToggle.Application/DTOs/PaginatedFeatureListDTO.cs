using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureToggle.Application.DTOs
{
    public class PaginatedFeatureListDTO
    {
        public int PageSize {  get; set; }
        public int FeatureCount { get; set; }
        public int TotalPages { get; set; }
        public List<FilteredFeatureDTO> FeatureList { get; set; }
    }
}
