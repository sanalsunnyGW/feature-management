namespace FeatureToggle.Domain.Entity.BusinessSchema
{
    public class Business
    {
        public int BusinessId { get; private set; }
        public string BusinessName { get; private set; }
        
        //public List<BusinessFeatureFlag>? BusinessFeatures { get; private set; }

    }
}
