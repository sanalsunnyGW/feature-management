using FeatureToggle.Domain.Entity.BusinessSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureToggle.Domain.Configurations
{
    public class BusinessFeatureFlagConfiguration : IEntityTypeConfiguration<BusinessFeatureFlag>
    {
        public void Configure(EntityTypeBuilder<BusinessFeatureFlag> builder)
        {
            builder.ToTable("BusinessFeatureFlag", "business");
            builder.HasKey(x => x.FeatureFlagId);

            builder
            .HasOne(bf => bf.Business) 
            .WithMany(x => x.BusinessFeatures) 
            .HasForeignKey(bf => bf.BusinessId); 

           
            builder
                .HasOne(bf => bf.Feature)
                .WithMany(x => x.BusinessFeatures) 
                .HasForeignKey(bf => bf.FeatureId);
        }
    }
}
