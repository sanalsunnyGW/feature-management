using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.BusinessSchema;

namespace FeatureToggle.Application.DTOs
{
    public class FilteredFeatureDTO
    {
        public int? FeatureFlagId { get; set; }
        public int? FeatureId { get; set; }
        public string FeatureName { get; set; } = string.Empty;
        public int FeatureType { get; set; }
        public bool? IsEnabled { get; set; }

    }
}
