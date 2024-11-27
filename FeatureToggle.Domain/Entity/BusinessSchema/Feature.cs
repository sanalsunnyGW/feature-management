namespace FeatureToggle.Domain.Entity.BusinessSchema
{
    public class Feature
    {
        public int FeatureId { get; private set; }
        public string FeatureName { get; private set; }
        //public List<BusinessFeatureFlag> BusinessFeatures { get; private set; }
        public FeatureType FeatureType { get; private set; }
        public int FeatureTypeId { get; private set; }
    }
}
