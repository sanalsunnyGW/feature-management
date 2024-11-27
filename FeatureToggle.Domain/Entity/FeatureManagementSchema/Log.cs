using System.Globalization;
using Microsoft.AspNetCore.SignalR;

namespace FeatureToggle.Domain.Entity.FeatureManagementSchema
{
    public class Log
    {
        public int Id { get; private set; }
        public string UserId { get; private set; }     
        
        public string UserName { get; private set; }
        public int FeatureId { get; private set; }

        public string FeatureName { get; private set; }
        public int? BusinessId { get; private set; }
       
        public string? BusinessName { get; private set; }  
        public DateTime Time { get; private set; }
        public Actions Action { get; private set; }

        public Log(string userId, string userName, int featureId, string featureName, int? businessId, string? businessName ,Actions action)
        {
            UserId = userId;
            UserName = userName;
            FeatureId = featureId;
            FeatureName = featureName;
            BusinessId = businessId;
            BusinessName = businessName;
            Action = action;
            Time = DateTime.Now;

        }

    }
}
