using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Configurations
{
    public class DeliveryRegionConfiguration : IEntityTypeConfiguration<DeliveryRegion>
    {
        public void Configure(EntityTypeBuilder<DeliveryRegion> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
