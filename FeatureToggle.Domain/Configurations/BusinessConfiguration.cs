using FeatureToggle.Domain.Entity.BusinessSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureToggle.Domain.Configurations
{
    public class BusinessConfiguration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.ToTable("Business", "business");

            builder.Property(x => x.BusinessName).IsRequired()
                    .HasColumnType("nvarchar").HasMaxLength(20);

            //builder.HasMany(x => x.BusinessFeatures)
            //    .WithOne(x => x.Business)
            //    .HasForeignKey(x => x.BusinessId);
        }
    }
}
