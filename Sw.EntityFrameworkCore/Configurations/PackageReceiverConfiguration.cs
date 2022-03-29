using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Configurations
{
    public class PackageReceiverConfiguration : IEntityTypeConfiguration<PackageReceiver>
    {
        public void Configure(EntityTypeBuilder<PackageReceiver> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
