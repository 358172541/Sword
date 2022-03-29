using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Configurations
{
    public class MemberDeliveryAddressConfiguration : IEntityTypeConfiguration<MemberDeliveryAddress>
    {
        public void Configure(EntityTypeBuilder<MemberDeliveryAddress> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
