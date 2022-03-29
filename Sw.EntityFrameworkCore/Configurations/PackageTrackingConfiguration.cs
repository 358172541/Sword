using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Configurations
{
    public class PackageTrackingConfiguration : IEntityTypeConfiguration<PackageTracking>
    {
        public void Configure(EntityTypeBuilder<PackageTracking> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
