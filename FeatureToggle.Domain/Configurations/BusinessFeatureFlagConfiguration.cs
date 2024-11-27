using System.Reflection.Emit;
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
            .HasOne(bf => bf.Business) // BusinessFeatureFlag has one Business
            .WithMany() // Business can have many BusinessFeatureFlags (no navigation property)
            .HasForeignKey(bf => bf.BusinessId); // BusinessId is the foreign key

            // Configure BusinessFeatureFlag-Feature relationship
            builder
                .HasOne(bf => bf.Feature) // BusinessFeatureFlag has one Feature
                .WithMany() // Feature can have many BusinessFeatureFlags (no navigation property)
                .HasForeignKey(bf => bf.FeatureId);
        }
    }
}
