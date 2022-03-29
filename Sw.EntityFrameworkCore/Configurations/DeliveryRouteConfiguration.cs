using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Configurations
{
    public class DeliveryRouteConfiguration : IEntityTypeConfiguration<DeliveryRoute>
    {
        public void Configure(EntityTypeBuilder<DeliveryRoute> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
