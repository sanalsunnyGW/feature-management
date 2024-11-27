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
        public int FeatureFlagId { get; set; }
        public int? FeatureId { get; set; }
        public string FeatureName { get; set; }
        //public int? BusinessId { get; set; }
        //public string? BusinessName { get; set; }
        public int FeatureType { get; set; }
        public bool? isEnabled { get; set; }

        

        //public bool isEnabled {  get; set; }
        /*
        public int FeatureFlagId { get; private set; }
        public Business? Business { get; private set; }
        public int BusinessId { get; private set; }
        public Feature? Feature { get; private set; }
        public int FeatureId { get; private set; }
        public bool IsEnabled { get; private set; }
        */

    }
}
